using Adapter.Azure.Blob;
using Hexagonal.Common.Constants;
using Hexagonal.Domain.Entities.Users;
using Hexagonal.Domain.Views.Users;

namespace Hexagonal.DTO.Response.Users
{
    public partial record GetAllResponse
    {
        public static implicit operator GetAllResponse(GetAllView u)
        {
            if (u is null)
                return null;

            return new GetAllResponse
            {
                Id = u.Id,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,                
                LastSignIn = u.LastSignIn,

                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone,
                Status = u.Status,

                ProfileImage = u.ProfileImage,
                ProfileImageUrl = BlobPathHelper.GetUrl(BlobContainer.Profile, u.ProfileImage),

                ProfileId = u.ProfileId,
                ProfileName = u.ProfileName,
                HasOrderAccess = u.HasOrdersAccess,
                HasSchoolAccess = u.HasSchoolAccess,
                HasStockAccess = u.HasStockAccess,
            };
        }
    }
}
