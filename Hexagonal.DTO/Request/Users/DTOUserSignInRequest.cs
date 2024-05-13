namespace Hexagonal.DTOs.Request.Users
{
    public class DTOUserSignInRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
