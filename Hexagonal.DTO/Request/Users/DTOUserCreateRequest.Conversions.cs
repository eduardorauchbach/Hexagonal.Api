using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Request.Users
{
    public partial class DTOUserCreateRequest
    {
        public User ToDomain() => (User)this;
    }
}
