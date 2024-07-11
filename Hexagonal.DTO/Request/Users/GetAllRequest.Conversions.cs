using Hexagonal.Domain.Filters.Users;

namespace Hexagonal.DTO.Request.Users
{
    public partial record GetAllRequest
    {
        public override GetAllFilter ToPageFilter() => (GetAllFilter)this;
    }

}
