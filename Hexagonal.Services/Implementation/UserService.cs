using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Common.Extensions;
using Hexagonal.Common.Pagging;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.DTO.Request.Users;
using Hexagonal.DTO.Response.Users;
using Hexagonal.Repositories;
using RauchTech.Logging;
using RauchTech.Logging.Aspects;
using System.Net;

namespace Hexagonal.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly ICustomLog<UserService> _log;

        private readonly IUserRepository _userRepository;

        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;
        private readonly IBlobService _blobService;
        private readonly IProfileRepository _profileRepository;

        public UserService(
            ICustomLog<UserService> log,
            IUserRepository userRepository,
            IHashService hashService,
            ITokenService tokenService,
            IBlobService blobService,
            IProfileRepository profileRepository)
        {
            _log = log;

            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _blobService = blobService;
            _profileRepository = profileRepository;
        }

        [LogAspect]
        public async Task<Result<Response>> Create(CreateRequest request)
        {
            var oldUser = await _userRepository.Get(request.Email);
            if (oldUser is not null && oldUser.IsCompleted)
            {
                return Result.Failure(Messages.UserAlreadyExists, HttpStatusCode.Conflict);
            }

            var user = request.ToDomain();

            user.Id = oldUser?.Id;

            user.NewId();
            user.Password = _hashService.HashValue(user.Password);
            user.IsCompleted = true;

            if (oldUser is null)
            {
                await _userRepository.Create(user);
            }
            else
            {
                await _userRepository.Update(user);
            }

            return Result.Success(user.ToResponse());
        }

        /// <summary>
        /// Create a temporary user that is not set yet, only for link purposes
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [LogAspect]
        public async Task<User> CreateClean(string email)
        {
            var user = new User
            {
                Email = email
            };

            user.NewId();
            await _userRepository.Create(user);

            return user;
        }

        [LogAspect]
        public async Task<Result<Response>> Get(Guid id)
        {
            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            return Result.Success(user.ToResponse());
        }

        [LogAspect]
        public async Task<Result<PageResponse<GetAllResponse>>> GetAll(GetAllRequest request)
        {
            var usersPage = await _userRepository.GetAll(request.ToPageFilter());

            var response = usersPage.ConvertItems(user => user.ToResponse());
            return Result.Success(response);
        }

        [LogAspect]
        public async Task<Result<Response>> EditPassword(string email, EditPasswordRequest request)
        {
            var user = await _userRepository.Get(email);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Password = _hashService.HashValue(request.Password);
            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToResponse());
        }

        [LogAspect]
        public async Task<Result<Response>> EditProfileImage(Guid id, EditProfileImageRequest request)
        {
            if (request.FileData is null)
            {
                return Result.Failure(Messages.GenericInvalidData, HttpStatusCode.UnprocessableContent);
            }

            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            var fileData = request.FileData.OpenReadStream();
            var extension = request.FileData.FileName.GetExtension();

            if (user.ProfileImage is null)
            {
                user.ProfileImage = await _blobService.Create(BlobContainer.Profile, extension, fileData);
            }
            else
            {
                user.ProfileImage = await _blobService.Update(BlobContainer.Profile, user.ProfileImage, extension, fileData);
            }

            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToResponse());
        }

        [LogAspect]
        public async Task<Result<Response>> EditStatus(Guid id, EditStatusRequest request)
        {
            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Status = request.Status;
            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToResponse());
        }

        /// <summary>
        /// Edit only the properties that are not null
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [LogAspect]
        public async Task<Result<Response>> Patch(Guid id, PatchRequest request)
        {
            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.Phone = request.Phone ?? user.Phone;
            user.Status = request.Status ?? user.Status;
            user.ProfileId = request.ProfileId ?? user.ProfileId;

            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToResponse());
        }

        [LogAspect]
        public async Task<Result<SignInResponse>> SignIn(SignInRequest login)
        {
            User? user = await _userRepository.Get(login.Email);

            if (user is null || !user.IsAdmin)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            var isPasswordVerify = _hashService.VerifyValue(login.Password, user.Password);

            if (!isPasswordVerify)
            {
                return Result.Failure(Messages.UserOrPasswordInvalid, HttpStatusCode.Unauthorized);
            }

            user.LastSignIn = DateTime.UtcNow;
            await _userRepository.Update(user);

            var userResponse = user.ToResponse();

            if (userResponse.ProfileId.HasValue)
                userResponse.Profile = await _profileRepository.Get(user.ProfileId.Value);

            var response = new SignInResponse
            {
                User = userResponse,
                Token = _tokenService.GenerateToken(userResponse)
            };

            return Result.Success(response);
        }
    }
}
