using Hexagonal.Common.DTO;
using Hexagonal.DTO.Request.Profiles;
using Hexagonal.DTO.Response.Profiles;

namespace Hexagonal.Services
{
    public interface IProfileService
    {
        Task<Result<Response>> Create(CreateRequest request);
        Task<Result<Response>> Delete(Guid id);
        Task<Result<Response>> Get(Guid id);
        Task<Result<IEnumerable<Response>>> GetAll();
        Task<Result<Response>> Patch(Guid id, PatchRequest request);
    }
}