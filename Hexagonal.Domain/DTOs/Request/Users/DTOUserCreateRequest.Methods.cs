using Hexagonal.Domain.Domain.Entities.Users;

namespace Hexagonal.Domain.DTOs.Request.Users
{
    public partial class DTOUserCreateRequest
    {
        public User ToDomain() => (User)this;
    }
}
