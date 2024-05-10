using Hexagonal.Domain.Domain.Entities.Verifications;

namespace Hexagonal.Domain.DTOs.Request.Verifications
{
    public partial class DTOVerificationCreateRequest
    {
        public string Value { get; set; }
        public VerificationType Type { get; set; }
    }
}
