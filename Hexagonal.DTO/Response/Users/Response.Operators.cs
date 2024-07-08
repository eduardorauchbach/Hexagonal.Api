using Adapter.Azure.Blob;
using Hexagonal.Common.Constants;
using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTO.Response.Users
{
    public partial record Response
    {
        public static implicit operator Response(User u)
        {
            if (u is null)
                return null;

            return new Response
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                ProfileId = u.ProfileId,
                LastSignIn = u.LastSignIn,
                Name = u.Name,
                Email = u.Email,
                Status = u.Status,
                IsAdmin = u.IsAdmin,
                ProfileImage = u.ProfileImage,
                ProfileImageUrl = BlobPathHelper.GetUrl(BlobContainer.Profile, u.ProfileImage),
            };
        }
    }
}
