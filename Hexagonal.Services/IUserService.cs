using Hexagonal.Common.DTO;
using Hexagonal.Domain.Domain.Entities.Users;
using Hexagonal.Domain.DTOs.Request.Users;
using Hexagonal.Domain.DTOs.Response.Users;

namespace Hexagonal.Services
{
    public interface IUserService
    {
        Task<Result<DTOUserResponse>> Create(DTOUserCreateRequest request);
        Task<User> CreateClean(string email);
        Task<Result<DTOUserResponse>> EditPassword(string email, DTOUserEditPasswordRequest request);
        Task<Result<DTOUserResponse>> EditProfileImage(Guid id, DTOUserEditProfileImageRequest request);
        Task<Result<DTOUserResponse>> EditStatus(Guid id, DTOUserEditStatusRequest request);
        Task<Result<DTOUserResponse>> Get(Guid id);
        Task<Result<DTOUserResponse>> Patch(Guid id, DTOUserPatchRequest request);
        Task<Result<DTOUserSignInResponse>> SignIn(DTOUserSignInRequest login);
    }
}