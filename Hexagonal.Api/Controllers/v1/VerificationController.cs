using Hexagonal.Api.Controllers.Helper;
using Hexagonal.DTOs.Request.Verifications;
using Hexagonal.DTOs.Response.Verifications;
using Hexagonal.Services;
using Hexagonal.Session;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hexagonal.Api.Controllers.v1.Common
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
        [SwaggerResponse(200, "Created verification", typeof(CreateResponse))]
        public async Task<ActionResult> CreateVerification([FromBody] CreateRequest request)
        {
            return await _verificationService.Create(request);
        }

        [HttpPost("Validate", Name = nameof(ValidateVerification))]
        [SwaggerOperation(Summary = "Validate a verification", Description = "Validate the code of a verification")]
        [SwaggerResponse(200, "Validation result", typeof(ValidateResponse))]
        public async Task<ActionResult> ValidateVerification([FromBody] ValidateRequest request)
        {
            return await _verificationService.Validate(request);
        }
    }
}
