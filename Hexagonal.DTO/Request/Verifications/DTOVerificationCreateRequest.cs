using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Request.Verifications
{
    public partial class DTOVerificationCreateRequest
    {
        public string Value { get; set; }
        public VerificationType Type { get; set; }
    }
}
