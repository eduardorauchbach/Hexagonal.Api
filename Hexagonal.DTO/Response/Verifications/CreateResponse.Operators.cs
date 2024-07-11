using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Response.Verifications
{
    public partial record CreateResponse
    {
        public static explicit operator CreateResponse(Verification v)
        {
            if (v == null)
                return null;

            return new CreateResponse
            {
                Id = v.Id,
                CreatedAt = v.CreatedAt,
                UpdatedAt = v.UpdatedAt,
                ExpiresAt = v.ExpiresAt,
            };
        }
    }
}
