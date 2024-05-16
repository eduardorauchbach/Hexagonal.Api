using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Request.Users
{
    public partial record DTOUserCreateRequest
    {
        public User ToDomain() => (User)this;
    }
}
