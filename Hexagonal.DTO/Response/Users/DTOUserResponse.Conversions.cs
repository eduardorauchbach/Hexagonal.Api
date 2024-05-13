using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Response.Users
{
    public static class DTOUserResponseConversions
    {
        public static DTOUserResponse ToDTOResponse(this User value)
        {
            return (DTOUserResponse)value;
        }

        public static IList<DTOUserResponse> ToDTOListResponse(IList<User> values)
            => values.Select(b => b.ToDTOResponse()).ToList();
    }
}
