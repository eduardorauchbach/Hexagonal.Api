using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Entities.Verifications;

namespace Hexagonal.DTOs.Response.Verifications
{
    public static class DTOVerificationCreateResponseConversions
    {
        public static DTOVerificationCreateResponse ToDTOResponse(this Verification value) => (DTOVerificationCreateResponse)value;

        public static IList<DTOVerificationCreateResponse> ToDTOResponse(IList<Verification> values)
            => values.Select(b => b.ToDTOResponse()).ToList();
    }
}
