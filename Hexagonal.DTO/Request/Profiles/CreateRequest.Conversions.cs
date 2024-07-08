using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Request.Profiles
{
    public partial record CreateRequest
    {
        public Profile ToDomain() => (Profile)this;
    }
}
