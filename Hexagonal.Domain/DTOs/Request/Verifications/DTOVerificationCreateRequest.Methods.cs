using Hexagonal.Domain.Domain.Entities.Users;
using Hexagonal.Domain.Domain.Entities.Verifications;

namespace Hexagonal.Domain.DTOs.Request.Verifications
{
    public partial class DTOVerificationCreateRequest
    {
        public Verification ToDomain() => (Verification)this;
    }
}
