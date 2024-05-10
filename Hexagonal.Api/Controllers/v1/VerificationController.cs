using GestaoVarejoTwoS.Api.Controllers.Helper;
using Microsoft.AspNetCore.Mvc;
using Hexagonal.Domain.DTOs.Request.Verifications;
using Hexagonal.Domain.DTOs.Response.Verifications;
using Hexagonal.Services;
using Hexagonal.Session;
using Swashbuckle.AspNetCore.Annotations;

namespace Hexagonal.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VerificationController : CustomControllerBase
    {
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _verificationService = verificationService;
        }

        [HttpPost("", Name = nameof(CreateVerification))]
        [SwaggerOperation(Summary = "Create a new verification", Description = "Creates a verification")]
        [SwaggerResponse(200, "Created verification", typeof(DTOVerificationCreateResponse))]
        public async Task<ActionResult<DTOVerificationCreateResponse>> CreateVerification([FromBody] DTOVerificationCreateRequest request)
        {
            var response = await _verificationService.Create(request);

            return Ok(response);
        }

        [HttpPost("Validate", Name = nameof(ValidateVerification))]
        [SwaggerOperation(Summary = "Validate a verification", Description = "Validate the code of a verification")]
        [SwaggerResponse(200, "Validation result", typeof(DTOVerificationValidateResponse))]
        public async Task<ActionResult<DTOVerificationValidateResponse>> ValidateVerification([FromBody] DTOVerificationValidateRequest request)
        {
            var response = await _verificationService.Validate(request);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
