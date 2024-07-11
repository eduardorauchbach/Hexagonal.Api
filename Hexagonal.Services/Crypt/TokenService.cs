using Hexagonal.Common.Configurations;
using Hexagonal.Domain.Entities.Profiles;
using Hexagonal.Domain.Entities.Users;
using UserResponse = Hexagonal.DTO.Response.Users.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hexagonal.Services.Crypt
{
    internal class TokenService : ITokenService
    {
        public string GenerateAnonymousToken(string value, int hours = 1)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthorizationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Anonymous, value),
                }),
                Expires = DateTime.UtcNow.AddHours(hours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateAnonymousTokenInfo(HttpContext context, string type, string value)
        {
            var comparingValue = context.User.FindFirst(type);

            return (comparingValue is not null && comparingValue.Value == value);
        }


        public string GenerateToken(UserResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AuthorizationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public UserResponse GetTokenUserData(HttpContext context)
        {
            var userData = context.User.FindFirst(ClaimTypes.UserData);

            return (userData is not null ? JsonConvert.DeserializeObject<User>(userData.Value)! : null);
        }

        public bool IsAdmin(HttpContext context)
        {
            var userData = GetTokenUserData(context);
            return userData?.IsAdmin ?? true; //Null significa dev, pois do contrário o controle de Token nativo vai barrar
        }

        public bool HasPermission(HttpContext context, ProfileAreaType area, bool add = false, bool update = false, bool delete = false)
        {
            var userData = GetTokenUserData(context);
            if (userData is null) return true; //Null significa dev, pois do contrário o controle de Token nativo vai barrar

            var profileArea = userData?.Profile?.ProfileAreas.FirstOrDefault(x => x.Area == area);

            if (profileArea is null)
            {
                var result = true;

                if (add)
                {
                    result &= profileArea.CanAdd;
                }
                if (update)
                {
                    result &= profileArea.CanUpdate;
                }
                if (delete)
                {
                    result &= profileArea.CanDelete;
                }

                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
