using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Request.Verifications
{
    public partial record CreateRequest
    {
        public static implicit operator Verification(CreateRequest dto)
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
