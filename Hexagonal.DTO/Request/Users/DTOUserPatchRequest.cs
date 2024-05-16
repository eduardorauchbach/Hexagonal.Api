namespace Hexagonal.DTOs.Request.Users
{
    public record DTOUserPatchRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? StatusInfo { get; set; }
    }
}
