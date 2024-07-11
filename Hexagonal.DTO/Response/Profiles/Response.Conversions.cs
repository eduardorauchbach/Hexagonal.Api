using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Profiles
{
    public static class ProfileResponseConversions
    {
        public static Response ToResponse(this Profile value) => (Response)value;

        public static IEnumerable<Response> ToResponse(this IEnumerable<Profile> values)
            => values.Select(b => b.ToResponse());
    }
}
