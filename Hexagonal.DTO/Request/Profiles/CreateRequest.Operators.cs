using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Request.Profiles;

namespace Hexagonal.DTO.Request.Profiles
{
    public partial record CreateRequest
    {
        public static implicit operator Profile(CreateRequest dto)
        {
            if (dto is null)
                return null;

            return new Profile
            {
                Name = dto.Name
            };
        }
    }
}
