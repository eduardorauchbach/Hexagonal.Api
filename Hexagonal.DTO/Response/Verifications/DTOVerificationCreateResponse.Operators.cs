using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Response.Verifications
{
    public partial record DTOVerificationCreateResponse
    {
        public static explicit operator DTOVerificationCreateResponse(Verification v)
        {
            if (v == null)
                return null;

            return new DTOVerificationCreateResponse
            {
                Id = v.Id,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                ExpiresAt = v.ExpiresAt,
            };
        }
    }
}
