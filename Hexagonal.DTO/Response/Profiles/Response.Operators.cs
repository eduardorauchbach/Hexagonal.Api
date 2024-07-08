using Hexagonal.Domain.Entities.Profiles;

namespace Hexagonal.DTO.Response.Profiles
{
    public partial record Response
    {
        public static implicit operator Response(Profile profile)
        {
            if (profile is null)
                return null;

            return new Response
            {
                Id = profile.Id,
                Name = profile.Name,
                ProfileAreas = profile.ProfileAreas.Select(pa => (AreaResponse)pa).ToList(),
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt
            };
        }
    }
}
