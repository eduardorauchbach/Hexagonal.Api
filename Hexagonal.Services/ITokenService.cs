using Hexagonal.Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Hexagonal.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        string GenerateAnonymousToken(string value, int hours = 1);
        bool ValidateTokenInfo(HttpContext context, string type, string value);
    }
}