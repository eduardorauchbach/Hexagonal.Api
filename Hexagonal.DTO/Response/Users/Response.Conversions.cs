using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTO.Response.Users
{
    public static class UserResponseConversions
    {
        public static Response ToResponse(this User value) => (Response)value;

        public static IEnumerable<Response> ToResponse(this IEnumerable<User> values)
            => values.Select(b => b.ToResponse());
    }
}
