using Hexagonal.Common.Entities;

namespace Hexagonal.Domain.Entities.Users
{
    public partial class User : EntityBase
    {
        public string Name { get; set; }
        public string? Document { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? LastSignIn { get; set; }
        public UserStatus Status { get; set; }
        public bool IsCompleted { get; set; }

        public Guid? ProfileId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
