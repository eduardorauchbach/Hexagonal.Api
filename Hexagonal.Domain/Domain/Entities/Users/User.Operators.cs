using Hexagonal.Domain.DTOs.Request.Users;

namespace Hexagonal.Domain.Domain.Entities.Users
{
    public partial class User
    {
        public static explicit operator User(DTOUserCreateRequest dto)
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
