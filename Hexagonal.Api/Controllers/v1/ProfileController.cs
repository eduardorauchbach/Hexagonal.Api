using Hexagonal.Api.Controllers.Helper;
using Hexagonal.Common.DTO;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Request.Profiles;
using Hexagonal.DTO.Response.Profiles;
using Hexagonal.Services;
using Hexagonal.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hexagonal.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfileController : CustomControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IUnitOfWork unitOfWork, ITokenService tokenService, IProfileService profileService) : base(unitOfWork, tokenService)
        {
            _profileService = profileService;
        }

        [HttpPost("", Name = nameof(CreateProfile))]
        [Authorize]
        [SwaggerOperation(Summary = "Create a new profile", Description = "Creates a new profile and returns it")]
        [SwaggerResponse(200, "Created profile", typeof(Response))]
        public async Task<ActionResult> CreateProfile([FromBody] CreateRequest request)
        {
            if (!HasPermission(ProfileAreaType.Profiles, out var result, add: true)) return result;

            return await _profileService.Create(request);
        }

        [HttpGet("{id:guid}", Name = nameof(GetProfileById))]
        [Authorize]
        [SwaggerOperation(Summary = "Get profile by Id", Description = "Profile Details by ID")]
        [SwaggerResponse(200, "Profile found", typeof(Response))]
        public async Task<ActionResult> GetProfileById(
            [FromRoute, SwaggerParameter("ID of the Profile", Required = true)] Guid id)
        {
            //TODO: Validate Access

            return await _profileService.Get(id);
        }

        [HttpGet("", Name = nameof(GetProfiles))]
        [Authorize]
        [SwaggerResponse(200, "Profiles found", typeof(Response))]
        public async Task<ActionResult> GetProfiles()
        {
            //TODO: Validate Access

            return await _profileService.GetAll();
        }

        [HttpPatch("{id}", Name = nameof(PatchProfile))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit profile", Description = "Edit profile, only changing values that are not null")]
        [SwaggerResponse(200, "Edited profile", typeof(Response))]
        public async Task<ActionResult> PatchProfile(Guid id, [FromBody] PatchRequest request)
        {
            if (!HasPermission(ProfileAreaType.Profiles, out var result, update: true)) return result;

            return await _profileService.Patch(id, request);
        }

        [HttpDelete("{id:guid}", Name = nameof(DeleteProfile))]
        [Authorize]
        [SwaggerOperation(Summary = "Delete profile by Id", Description = "Delete Profile by ID")]
        [SwaggerResponse(200, "Profile deleted", typeof(Result))]
        public async Task<ActionResult> DeleteProfile(
            [FromRoute, SwaggerParameter("ID of the Profile", Required = true)] Guid id)
        {
            if (!HasPermission(ProfileAreaType.Profiles, out var result, delete: true)) return result;

            return await _profileService.Delete(id);
        }
    }
}
