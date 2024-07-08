using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Request.Profiles;
using Hexagonal.DTO.Response.Profiles;
using Hexagonal.Repositories;
using RauchTech.Logging;
using RauchTech.Logging.Aspects;
using System.Net;

namespace Hexagonal.Services.Implementation
{
    internal class ProfileService : IProfileService
    {
        private readonly ICustomLog<ProfileService> _log;
        private readonly IProfileRepository _profileRepository;

        public ProfileService(
            ICustomLog<ProfileService> log,
            IProfileRepository profileRepository)
        {
            _log = log;
            _profileRepository = profileRepository;
        }

        [LogAspect]
        public async Task<Result<Response>> Create(CreateRequest request)
        {
            var existingProfile = await _profileRepository.Get(request.Name);
            if (existingProfile is not null)
            {
                return Result.Failure(Messages.ProfileNameAlreadyExists, HttpStatusCode.Conflict);
            }

            var profile = request.ToDomain();
            profile.NewId();

            // Automatically create profile areas
            profile.ProfileAreas = new List<ProfileArea>
            {
                new ProfileArea { ProfileId = profile.Id.Value, Area = ProfileAreaType.Users, CanAdd = false, CanUpdate = false, CanDelete = false },
                new ProfileArea { ProfileId = profile.Id.Value, Area = ProfileAreaType.Profiles, CanAdd = false, CanUpdate = false, CanDelete = false },
            };

            await _profileRepository.Create(profile);

            return Result.Success(profile.ToResponse());
        }

        [LogAspect]
        public async Task<Result<Response>> Delete(Guid id)
        {
            await _profileRepository.Delete(id);

            return Result.Success();
        }

        [LogAspect]
        public async Task<Result<Response>> Get(Guid id)
        {
            var profile = await _profileRepository.Get(id);
            if (profile is null)
            {
                return Result.Failure(Messages.ProfileNotFound, HttpStatusCode.NotFound);
            }

            return Result.Success(profile.ToResponse());
        }

        [LogAspect]
        public async Task<Result<IEnumerable<Response>>> GetAll()
        {
            var profile = await _profileRepository.GetAll();

            return Result.Success(profile.ToResponse());
        }

        [LogAspect]
        public async Task<Result<Response>> Patch(Guid id, PatchRequest request)
        {
            var profile = await _profileRepository.Get(id);
            if (profile is null)
            {
                return Result.Failure(Messages.ProfileNotFound, HttpStatusCode.NotFound);
            }

            if (profile.Name != null && request.Name != profile.Name)
            {
                var existingProfile = await _profileRepository.Get(request.Name);
                if (existingProfile is not null)
                {
                    return Result.Failure(Messages.ProfileNameAlreadyExists, HttpStatusCode.Conflict);
                }
            }

            profile.Name = request.Name ?? profile.Name;
            profile.ProfileAreas = request.ProfileAreas.ToDomain() ?? profile.ProfileAreas;
            profile.Update();

            await _profileRepository.Update(profile);

            return Result.Success(profile.ToResponse());
        }
    }
}
