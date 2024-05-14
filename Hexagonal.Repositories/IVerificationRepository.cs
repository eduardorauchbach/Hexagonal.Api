using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.Repositories
{
    public interface IVerificationRepository
    {
        Task Create(Verification verification);
        Task<Verification> Get(Guid id);
    }
}