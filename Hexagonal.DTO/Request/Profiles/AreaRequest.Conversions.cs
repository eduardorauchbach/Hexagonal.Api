using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Response.Profiles;

namespace Hexagonal.DTO.Request.Profiles
{
    public static class ProfileAreaRequestConversions
    {
        public static ProfileArea ToDomain(this AreaRequest value) => (ProfileArea)value;

        public static List<ProfileArea> ToDomain(this IEnumerable<AreaRequest> values)
            => values.Select(b => b.ToDomain()).ToList();
    }
}
