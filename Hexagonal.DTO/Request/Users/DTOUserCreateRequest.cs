namespace Hexagonal.DTOs.Request.Users
{
    /// <summary>
    /// Represents a user creation request data transfer object.
    /// </summary>
    public partial record DTOUserCreateRequest
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The email number of the user.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        public required string Password { get; set; }
    }

}
