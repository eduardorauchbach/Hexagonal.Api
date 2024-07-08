namespace Hexagonal.DTO.Request.Users
{
    public record SignInRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
