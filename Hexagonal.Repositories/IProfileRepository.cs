using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.Repositories
{
    public interface IProfileRepository
    {
        Task Create(Profile profile);
        Task Delete(Guid id);
        Task<Profile> Get(Guid id);
        Task<Profile> Get(string name);
        Task<IEnumerable<Profile>> GetAll();
        Task Update(Profile profile);
    }
}