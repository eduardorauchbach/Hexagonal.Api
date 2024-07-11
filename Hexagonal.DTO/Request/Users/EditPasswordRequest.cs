namespace Hexagonal.DTO.Request.Users
{
    public record EditPasswordRequest
    {
        public required string Password { get; init; }
    }
}
