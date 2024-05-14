using Hexagonal.DTOs.Request.Verifications;
using Hexagonal.DTOs.Response.Verifications;

namespace Hexagonal.Services
{
    public interface IVerificationService
    {
        Task<DTOVerificationCreateResponse> Create(DTOVerificationCreateRequest request);
        Task<DTOVerificationValidateResponse> Validate(DTOVerificationValidateRequest request);
    }
}