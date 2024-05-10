using Hexagonal.Domain.DTOs.Response.Users;

namespace Hexagonal.Domain.Domain.Entities.Users
{
    public partial class User
    {
        public DTOUserResponse ToDTOResponse() => (DTOUserResponse)this;
        public static IList<DTOUserResponse> ToDTOListResponse(IList<User> users)
            => users.Select(b => b.ToDTOResponse()).ToList();
    }
}
