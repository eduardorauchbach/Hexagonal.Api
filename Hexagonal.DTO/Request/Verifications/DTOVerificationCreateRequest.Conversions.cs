using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Request.Verifications
{
    public partial class DTOVerificationCreateRequest
    {
        public Verification ToDomain() => (Verification)this;
    }
}
