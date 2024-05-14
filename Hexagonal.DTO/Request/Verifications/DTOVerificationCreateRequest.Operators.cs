using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Request.Verifications
{
    public partial class DTOVerificationCreateRequest
    {
        public static implicit operator Verification(DTOVerificationCreateRequest dto)
        {
            if (dto == null)
                return null;

            return new Verification
            {
                Origin = dto.Value,
                Type = dto.Type,
            };
        }
    }
}
