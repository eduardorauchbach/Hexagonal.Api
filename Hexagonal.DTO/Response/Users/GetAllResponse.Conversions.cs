using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Views.Users;

namespace Hexagonal.DTO.Response.Users
{
    public static class UserGetAllResponseConvertions
    {
        public static GetAllResponse ToResponse(this GetAllView value) => (GetAllResponse)value;

        public static IEnumerable<GetAllResponse> ToResponse(this IEnumerable<GetAllView> values)
            => values.Select(b => b.ToResponse());
    }
}
