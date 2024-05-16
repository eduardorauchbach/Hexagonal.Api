namespace Hexagonal.DTOs.Request.Users
{
    public record DTOUserEditPasswordRequest
    {
        public required string Password { get; set; }
    }
}
