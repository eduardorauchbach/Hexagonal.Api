using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTO.Request.Users
{
    public partial record CreateRequest
    {
        public User ToDomain() => (User)this;
    }
}
