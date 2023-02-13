using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoryHubAPI.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly UserManager<User> _userManager;
        private string secretKey = "extremelySecretKey"; // for now

        public AccessTokenService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ReadUserId(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "nameid");

            if (userIdClaim is null)
            {
                throw new Exception("Invalid access token.");
            }
            return userIdClaim.Value;
        }

        public string? RetrieveUserIdFromRequest(HttpRequest request)
        {
            if (request.Headers.ContainsKey("Authorization"))
            {
                if (request.Headers.TryGetValue("Authorization", out StringValues values))
                {
                    var jwt = values.ToString();

                    if (jwt.Contains("Bearer"))
                    {
                        jwt = jwt.Replace("Bearer", "").Trim();
                    }

                    try
                    {
                        return ReadUserId(jwt);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
