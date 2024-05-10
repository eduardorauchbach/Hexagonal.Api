using Hexagonal.Common.Entities;

namespace Hexagonal.Domain.Domain.Entities.Users
{
    public partial class User : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StatusInfo { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? LastSignIn { get; set; }
        public UserType Type { get; set; }
        public UserStatus Status { get; set; }
        public bool IsCompleted { get; set; }
    }
}
