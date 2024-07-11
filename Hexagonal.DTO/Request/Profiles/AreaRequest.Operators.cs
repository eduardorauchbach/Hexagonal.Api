using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Request.Profiles
{
    public partial record AreaRequest
    {
        public static implicit operator ProfileArea(AreaRequest dto)
        {
            if (dto is null)
                return null;

            return new ProfileArea
            {
                Area = dto.Area,
                CanAdd = dto.CanAdd,
                CanUpdate = dto.CanUpdate,
                CanDelete = dto.CanDelete
            };
        }
    }
}
