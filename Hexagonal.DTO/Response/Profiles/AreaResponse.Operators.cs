using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Profiles
{
    public partial record AreaResponse
    {
        public static implicit operator AreaResponse(ProfileArea profileArea)
        {
            if (profileArea is null)
                return null;

            return new AreaResponse
            {
                Area = profileArea.Area,
                CanAdd = profileArea.CanAdd,
                CanUpdate = profileArea.CanUpdate,
                CanDelete = profileArea.CanDelete
            };
        }
    }
}
