namespace Hexagonal.DTO.Request.Users
{
    /// <summary>
    /// Represents a user creation request data transfer object.
    /// </summary>
    public partial record CreateRequest
    {

        /// <summary>
        /// The profile for the adm user
        /// </summary>
        public required Guid ProfileId { get; init; }

        /// <summary>
        /// The user's Phone number
        /// </summary>
        public required string Phone { get; init; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// The email number of the user.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; init; }
    }

}
