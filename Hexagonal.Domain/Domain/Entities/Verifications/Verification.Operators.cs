using Hexagonal.Domain.DTOs.Request.Verifications;

namespace Hexagonal.Domain.Domain.Entities.Verifications
{
    public partial class Verification
    {
        public static explicit operator Verification(DTOVerificationCreateRequest dto)
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
