using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTOs.Request.Users
{
    public partial class DTOUserCreateRequest
    {
        public static implicit operator User(DTOUserCreateRequest dto)
        {
            if (dto is null)
                return null;

            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Status = UserStatus.Active,
                Password = dto.Password,
                Type = UserType.Regular,
            };
        }
    }
}
