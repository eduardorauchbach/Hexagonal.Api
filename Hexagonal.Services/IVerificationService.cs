using Hexagonal.Common.DTO;
using Hexagonal.DTOs.Request.Verifications;
using Hexagonal.DTOs.Response.Verifications;

namespace Hexagonal.Services
{
    public interface IVerificationService
    {
        Task<Result<DTOVerificationCreateResponse>> Create(DTOVerificationCreateRequest request);
        Task<Result<DTOVerificationValidateResponse>> Validate(DTOVerificationValidateRequest request);
    }
}