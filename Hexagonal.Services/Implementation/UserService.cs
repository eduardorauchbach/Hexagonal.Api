using Adapter.Azure.Blob.Configurations;
using Hexagonal.Common.Configurations;
using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Common.Extensions;
using Hexagonal.Domain.Domain.Entities.Users;
using Hexagonal.Domain.DTOs.Request.Users;
using Hexagonal.Domain.DTOs.Response.Users;
using Hexagonal.Repositories;
using Microsoft.Extensions.Options;
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

        public UserService(
            ICustomLog<UserService> log,
            IUserRepository userRepository,
            IHashService hashService,
            ITokenService tokenService,
            IBlobService blobService)
        {
            _log = log;

            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
            _blobService = blobService;
        }

        [LogAspect]
        public async Task<Result<DTOUserResponse>> Create(DTOUserCreateRequest request)
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

            return Result.Success(user.ToDTOResponse());
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
        public async Task<Result<DTOUserResponse>> Get(Guid id)
        {
            var user = await _userRepository.Get(id);
            return Result.Success(user.ToDTOResponse());
        }

        [LogAspect]
        public async Task<Result<DTOUserResponse>> EditPassword(string email, DTOUserEditPasswordRequest request)
        {
            var user = await _userRepository.Get(email);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Password = _hashService.HashValue(request.Password);
            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToDTOResponse());
        }

        [LogAspect]
        public async Task<Result<DTOUserResponse>> EditProfileImage(Guid id, DTOUserEditProfileImageRequest request)
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

            return Result.Success(user.ToDTOResponse());
        }

        [LogAspect]
        public async Task<Result<DTOUserResponse>> EditStatus(Guid id, DTOUserEditStatusRequest request)
        {
            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Status = request.Status;
            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToDTOResponse());
        }

        /// <summary>
        /// Edit only the properties that are not null
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="request"></param>
        /// <returns></returns>
        [LogAspect]
        public async Task<Result<DTOUserResponse>> Patch(Guid id, DTOUserPatchRequest request)
        {
            var user = await _userRepository.Get(id);
            if (user is null)
            {
                return Result.Failure(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.StatusInfo = request.StatusInfo ?? user.StatusInfo;
            user.Update();

            await _userRepository.Update(user);

            return Result.Success(user.ToDTOResponse());
        }

        [LogAspect]
        public async Task<Result<DTOUserSignInResponse>> SignIn(DTOUserSignInRequest login)
        {
            User? user = await _userRepository.Get(login.Email);

            if (user is null || !user.IsCompleted)
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

            var response = new DTOUserSignInResponse
            {
                User = user.ToDTOResponse(),
                Token = _tokenService.GenerateToken(user)
            };

            return Result.Success(response);
        }
    }
}
