using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Profiles
{
    public static class ProfileAreaResponseConversions
    {
        public static AreaResponse ToResponse(this ProfileArea value) => (AreaResponse)value;

        public static IEnumerable<AreaResponse> ToResponse(this IEnumerable<ProfileArea> values)
            => values.Select(b => b.ToResponse());
    }
}
