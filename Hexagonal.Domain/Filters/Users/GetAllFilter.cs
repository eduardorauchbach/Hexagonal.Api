using Hexagonal.Common.Paging;

namespace Hexagonal.Domain.Filters.Users
{
    public record GetAllFilter : PageFilter
    {
        public string? Name { get; init; }
        public bool IsAdmin { get; init; }
    }

}
