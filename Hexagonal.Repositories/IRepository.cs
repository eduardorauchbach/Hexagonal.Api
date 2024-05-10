using Hexagonal.Common.Entities;

namespace Hexagonal.Repositories
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<bool> Exists(Guid id);
        Task<T> Get(Guid id);
    }
}