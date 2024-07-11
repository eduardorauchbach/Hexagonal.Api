using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTO.Request.Users
{
    public partial record CreateRequest
    {
        public static implicit operator User(CreateRequest dto)
        {
            if (dto is null)
                return null;

            return new User
            {
                ProfileId = dto.ProfileId,
                Name = dto.Name,
                Email = dto.Email,
                Status = UserStatus.Active,
                Password = dto.Password,
                Phone = dto.Phone,
                IsAdmin = true,
                IsCompleted = true
            };
        }
    }
}
