using Hexagonal.Common.DTO;
using Hexagonal.DTOs.Request.Verifications;
using Hexagonal.DTOs.Response.Verifications;

namespace Hexagonal.Services
{
    public interface IVerificationService
    {
        Task<Result<CreateResponse>> Create(CreateRequest request);
        Task<Result<ValidateResponse>> Validate(ValidateRequest request);
    }
}