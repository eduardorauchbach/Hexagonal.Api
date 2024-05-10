using Hexagonal.Domain.Domain.Entities.Users;
using Hexagonal.Domain.Domain.Entities.Verifications;

namespace Hexagonal.Domain.DTOs.Response.Verifications
{
    public partial class DTOVerificationCreateResponse
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
