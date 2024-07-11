using Hexagonal.Common.DTO;
using Hexagonal.Common.Pagging;
using Hexagonal.Common.Paging;
using Hexagonal.Domain.Filters.Users;

namespace Hexagonal.DTO.Request.Users
{
    public partial record GetAllRequest
    {
        public static implicit operator GetAllFilter(GetAllRequest request)
        {
            if (request is null)
                return null;

            var pageFilter = PageRequest.ToPageFilter(request);

            return new GetAllFilter
            {
                Name = request.Name,
                IsAdmin = request.IsAdmin,
                OrderBy = pageFilter.OrderBy,
                Offset = pageFilter.Offset,
                PageSize = pageFilter.PageSize
            };
        }
    }

}
