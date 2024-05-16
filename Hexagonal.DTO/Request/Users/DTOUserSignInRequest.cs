namespace Hexagonal.DTOs.Request.Users
{
    public record DTOUserSignInRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
