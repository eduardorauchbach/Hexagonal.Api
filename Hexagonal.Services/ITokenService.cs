using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.DTO.Response.Users;
using Microsoft.AspNetCore.Http;

namespace Hexagonal.Services
{
    public interface ITokenService
    {
        string GenerateToken(Response user);
        string GenerateAnonymousToken(string value, int hours = 1);
        Response GetTokenUserData(HttpContext context);
        bool HasPermission(HttpContext context, ProfileAreaType area, bool add = false, bool update = false, bool delete = false);
        bool IsAdmin(HttpContext context);
        bool ValidateAnonymousTokenInfo(HttpContext context, string type, string value);
    }
}