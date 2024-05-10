using Adapter.Azure.Blob;
using Hexagonal.Common.Constants;
using Hexagonal.Domain.Domain.Entities.Users;

namespace Hexagonal.Domain.DTOs.Response.Users
{
    public partial class DTOUserResponse
    {
        public static explicit operator DTOUserResponse(User u)
        {
            if (u is null)
                return null;

            return new DTOUserResponse
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                LastSignIn = u.LastSignIn,
                Name = u.Name,
                Email = u.Email,
                Status = u.Status,
                StatusInfo = u.StatusInfo,
                ProfileImage = u.ProfileImage,
                ProfileImageUrl = BlobPathHelper.GetUrl(BlobContainer.Profile, u.ProfileImage),
                Type = u.Type,
            };
        }
    }
}
