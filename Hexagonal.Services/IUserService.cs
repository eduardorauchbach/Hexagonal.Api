using Hexagonal.Common.DTO;
using Hexagonal.Common.Pagging;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.DTO.Request.Users;
using Hexagonal.DTO.Response.Users;

namespace Hexagonal.Services
{
    public interface IUserService
    {
        Task<Result<Response>> Create(CreateRequest request);
        Task<User> CreateClean(string email);
        Task<Result<Response>> EditPassword(string email, EditPasswordRequest request);
        Task<Result<Response>> EditProfileImage(Guid id, EditProfileImageRequest request);
        Task<Result<Response>> EditStatus(Guid id, EditStatusRequest request);
        Task<Result<Response>> Get(Guid id);
        Task<Result<PageResponse<GetAllResponse>>> GetAll(GetAllRequest request);
        Task<Result<Response>> Patch(Guid id, PatchRequest request);
        Task<Result<SignInResponse>> SignIn(SignInRequest login);
    }
}