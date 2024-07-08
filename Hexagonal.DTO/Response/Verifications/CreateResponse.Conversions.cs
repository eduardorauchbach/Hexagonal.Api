using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Response.Verifications
{
    public static class DTOVerificationCreateResponseConversions
    {
        public static CreateResponse ToResponse(this Verification value) => (CreateResponse)value;

        public static IList<CreateResponse> ToResponse(IList<Verification> values)
            => values.Select(b => b.ToResponse()).ToList();
    }
}
