using Hexagonal.Domain.Domain.Entities.Users;

namespace Hexagonal.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task Create(User user);
        Task<User> Get(string email);
        Task Update(User user);
    }
}