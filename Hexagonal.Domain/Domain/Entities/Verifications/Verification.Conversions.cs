using Hexagonal.Domain.DTOs.Response.Users;
using Hexagonal.Domain.DTOs.Response.Verifications;

namespace Hexagonal.Domain.Domain.Entities.Verifications
{
    public partial class Verification
    {
        public DTOVerificationCreateResponse ToDTOResponse() => (DTOVerificationCreateResponse)this;
        public static IList<DTOVerificationCreateResponse> ToDTOListResponse(IList<Verification> verifications)
            => verifications.Select(b => b.ToDTOResponse()).ToList();
    }
}
