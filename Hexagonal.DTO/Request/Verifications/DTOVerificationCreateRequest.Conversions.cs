using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Request.Verifications
{
    public partial record DTOVerificationCreateRequest
    {
        public Verification ToDomain() => (Verification)this;
    }
}
