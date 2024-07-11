using Hexagonal.Common.Pagging;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Filters.Users;
using Hexagonal.Domain.Views.Users;

namespace Hexagonal.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<User> Get(Guid id);
        Task<User> Get(string email);
        Task<PageResponse<GetAllView>> GetAll(GetAllFilter filter);
        Task Update(User user);
    }
}