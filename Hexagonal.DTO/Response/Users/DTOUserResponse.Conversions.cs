using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Response.Users
{
    public static class DTOUserResponseConversions
    {
        public static DTOUserResponse ToDTOResponse(this User value) => (DTOUserResponse)value;

        public static IList<DTOUserResponse> ToDTOResponse(IList<User> values)
            => values.Select(b => b.ToDTOResponse()).ToList();
    }
}
