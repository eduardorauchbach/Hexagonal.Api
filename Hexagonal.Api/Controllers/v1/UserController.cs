using GestaoVarejoTwoS.Api.Controllers.Helper;
using Hexagonal.Api.Controllers.Helper;
using Hexagonal.Common.Constants;
using Hexagonal.Common.DTO;
using Hexagonal.Common.Entities;
using Hexagonal.Domain.DTOs.Request.Users;
using Hexagonal.Domain.DTOs.Response.Users;
using Hexagonal.Services;
using Hexagonal.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Claims;

namespace Hexagonal.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, IUnitOfWork unitOfWork, ITokenService tokenService) : base(unitOfWork)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("", Name = nameof(CreateUser))]
        [Authorize]
        [SwaggerOperation(Summary = "Create a new user", Description = "Creates a new user and returns it")]
        [SwaggerResponse(200, "Created user", typeof(DTOUserResponse))]
        public async Task<ActionResult> CreateUser([FromBody] DTOUserCreateRequest request)
        {
            if (!ValidateAnonymousEmailToken(request.Email))
            {
                return Result.Failure(Messages.TokenEmailValidationFailed, HttpStatusCode.Unauthorized).ToActionResult();
            }

            var response = await _userService.Create(request);
            return response.ToActionResult();
        }

        [HttpGet("{id:guid}", Name = nameof(GetUserById))]
        [Authorize]
        [SwaggerOperation(Summary = "Get user by Id", Description = "User Details by ID")]
        [SwaggerResponse(200, "User found", typeof(DTOUserResponse))]
        public async Task<ActionResult> GetUserById(
            [FromRoute, SwaggerParameter("ID of the User", Required = true)] Guid id)
        {
            //TODO: Validate Access

            var response = await _userService.Get(id);
            return response.ToActionResult();
        }

        [HttpPut("EditPassword/{email}", Name = nameof(EditPassword))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user password", Description = "Edit user password")]
        [SwaggerResponse(200, "Edited user", typeof(DTOUserResponse))]
        public async Task<ActionResult> EditPassword(string email, [FromBody] DTOUserEditPasswordRequest request)
        {
            if (!ValidateAnonymousEmailToken(email))
            {
                return Result.Failure(Messages.TokenEmailValidationFailed, HttpStatusCode.Unauthorized).ToActionResult();
            }

            var response = await _userService.EditPassword(email, request);
            return response.ToActionResult();
        }

        [HttpPut("EditStatus/{id}", Name = nameof(EditStatus))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user status", Description = "Edit user status")]
        [SwaggerResponse(200, "Edited user", typeof(DTOUserResponse))]
        public async Task<ActionResult> EditStatus(Guid id, [FromBody] DTOUserEditStatusRequest request)
        {
            //TODO: Validate Access

            var response = await _userService.EditStatus(id, request);
            return response.ToActionResult();
        }

        [HttpPut("EditProfileImage/{id}", Name = nameof(EditProfileImage))]
        [Authorize]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Edit user profile image", Description = "Edit user profile image")]
        [SwaggerResponse(200, "Edited user", typeof(DTOUserResponse))]
        public async Task<ActionResult> EditProfileImage(Guid id, [FromForm] DTOUserEditProfileImageRequest request)
        {
            //TODO: Validate userData of Token (id)

            var response = await _userService.EditProfileImage(id, request);
            return response.ToActionResult();
        }

        [HttpPatch("{id}", Name = nameof(PatchUser))]
        [Authorize]
        [SwaggerOperation(Summary = "Edit user", Description = "Edit user, only changing values that are not null")]
        [SwaggerResponse(200, "Edited user", typeof(DTOUserResponse))]
        public async Task<ActionResult> PatchUser(Guid id, [FromBody] DTOUserPatchRequest request)
        {
            //TODO: Validate userData of Token (id)

            var response = await _userService.Patch(id, request);
            return response.ToActionResult();
        }

        [HttpPost("SignIn", Name = nameof(SignIn))]
        [SwaggerOperation(Summary = "SignIn a user", Description = "SignIn a User and returns it with the token")]
        [SwaggerResponse(200, "User and Token", typeof(DTOUserSignInResponse))]
        public async Task<ActionResult> SignIn([FromBody] DTOUserSignInRequest request)
        {
            var response = await _userService.SignIn(request);
            return response.ToActionResult();
        }

        private bool ValidateAnonymousEmailToken(string email)
        {
            return _tokenService.ValidateTokenInfo(HttpContext, ClaimTypes.Anonymous, email);
        }
    }
}
