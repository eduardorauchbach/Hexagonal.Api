using Hexagonal.Api.Controllers.Helper;
using Hexagonal.Common.Pagging;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Request.Users;
using Hexagonal.DTO.Response.Users;
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
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUnitOfWork unitOfWork, ITokenService tokenService, IUserService userService) : base(unitOfWork, tokenService)
        {
            _userService = userService;
        }

        [HttpPost("", Name = nameof(CreateUser))]
        [Authorize]
        [SwaggerOperation(Summary = "Create a new user", Description = "Creates a new user and returns it")]
        [SwaggerResponse(200, "Created user", typeof(Response))]
        public async Task<ActionResult> CreateUser([FromBody] CreateRequest request)
        {
            //TODO: Validate Access

            return await _userService.Create(request);
        }

        [HttpGet("{id:guid}", Name = nameof(GetUserById))]
        [Authorize]
        [SwaggerOperation(Summary = "Get user by Id", Description = "User Details by ID")]
        [SwaggerResponse(200, "User found", typeof(Response))]
        public async Task<ActionResult> GetUserById(
            [FromRoute, SwaggerParameter("ID of the User", Required = true)] Guid id)
        {
            //TODO: Validate Access

            return await _userService.Get(id);
        }

        [HttpGet("", Name = nameof(GetAllUsers))]
        [Authorize]
        [SwaggerOperation(Summary = "Get all users", Description = "Get all users with pagination")]
        [SwaggerResponse(200, "Users found", typeof(PageResponse<GetAllResponse>))]
        public async Task<ActionResult> GetAllUsers([FromQuery] GetAllRequest request)
        {
            if (!HasPermission(out var result)) return result;

            return await _userService.GetAll(request);
        }


        [HttpPut("EditPassword/{email}", Name = nameof(EditPassword))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user password", Description = "Edit user password")]
        [SwaggerResponse(200, "Edited user", typeof(Response))]
        public async Task<ActionResult> EditPassword(string email, [FromBody] EditPasswordRequest request)
        {
            //TODO: Validate Access

            return await _userService.EditPassword(email, request);
        }

        [HttpPut("EditStatus/{id}", Name = nameof(EditStatus))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user status", Description = "Edit user status")]
        [SwaggerResponse(200, "Edited user", typeof(Response))]
        public async Task<ActionResult> EditStatus(Guid id, [FromBody] EditStatusRequest request)
        {
            if (!HasPermission(ProfileAreaType.Users, out var result, update: true)) return result;

            return await _userService.EditStatus(id, request);
        }

        [HttpPut("EditProfileImage/{id}", Name = nameof(EditProfileImage))]
        [Authorize]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Edit user profile image", Description = "Edit user profile image")]
        [SwaggerResponse(200, "Edited user", typeof(Response))]
        public async Task<ActionResult> EditProfileImage(Guid id, [FromForm] EditProfileImageRequest request)
        {
            //TODO: Validate userData of Token (id)

            return await _userService.EditProfileImage(id, request);
        }

        [HttpPatch("{id}", Name = nameof(PatchUser))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user", Description = "Edit user, only changing values that are not null")]
        [SwaggerResponse(200, "Edited user", typeof(Response))]
        public async Task<ActionResult> PatchUser(Guid id, [FromBody] PatchRequest request)
        {
            //TODO: Validate userData of Token (id)

            return await _userService.Patch(id, request);
        }

        [HttpPost("SignIn", Name = nameof(SignIn))]
        [SwaggerOperation(Summary = "SignIn a user", Description = "SignIn a User and returns it with the token")]
        [SwaggerResponse(200, "User and Token", typeof(SignInResponse))]
        public async Task<ActionResult> SignIn([FromBody] SignInRequest request)
        {
            return await _userService.SignIn(request);
        }
    }
}
