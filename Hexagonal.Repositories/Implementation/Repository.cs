using Hexagonal.Common.Entities;
using Hexagonal.Session;

namespace Hexagonal.Repositories
{
    internal abstract class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly IDbSession Session;

        public Repository(IDbSession session)
        {
            Session = session;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await Get(id) != null;
        }

        public abstract Task<T> Get(Guid id);
    }
}
