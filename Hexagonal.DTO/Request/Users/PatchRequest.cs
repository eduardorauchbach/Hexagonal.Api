using Hexagonal.Domain.Entities.Users;

namespace Hexagonal.DTO.Request.Users
{
    public record PatchRequest
    {
        /// <summary>
        /// The profile for the adm user
        /// </summary>
        public Guid? ProfileId { get; init; }

        /// <summary>
        /// The user's Phone
        /// </summary>
        public string? Phone { get; init; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string? Document { get; init; }

        /// <summary>
        /// The email number of the user.
        /// </summary>
        public string? Email { get; init; }

        /// <summary>
        /// user current status
        /// </summary>
        public UserStatus? Status { get; init; }
    }
}
