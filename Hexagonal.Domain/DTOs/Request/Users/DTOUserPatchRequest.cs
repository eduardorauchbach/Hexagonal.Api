namespace Hexagonal.Domain.DTOs.Request.Users
{
    public class DTOUserPatchRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? StatusInfo { get; set; }
    }
}
