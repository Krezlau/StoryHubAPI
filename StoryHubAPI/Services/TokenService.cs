using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoryHubAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly StoryHubDbContext _context;
        private string secretKey = "extremelySecretKey"; // for now

        public TokenService(UserManager<User> userManager, StoryHubDbContext context)
        {
            _userManager = userManager;
            _context = context;
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

        public async Task<string> RetrieveOrGenerateRefreshTokenAsync(User user)
        {
            var token = await RetrieveUserRefreshTokenAsync(user);

            if (token is not null && token.IsActive && !token.IsRevoked)
            {
                return token.Value;
            }

            if (token is not null)
            {
                _context.RefreshTokens.Remove(token);
                await _context.SaveChangesAsync();
            }
            return await GenerateRefreshTokenAsync(user); ;
        }

        public async Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken)
        {
            var dbToken = await RetrieveUserRefreshTokenAsync(user);
            if (dbToken is null || dbToken.IsRevoked || !dbToken.IsActive || refreshToken != dbToken.Value)
            {
                return false;
            }
            return true;
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

        private async Task<RefreshToken?> RetrieveUserRefreshTokenAsync(User user)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == user.Id);
        }

        private async Task<string> GenerateRefreshTokenAsync(User user)
        {
            string refreshTokenValue = RandomStringGeneration(25);
            var refreshToken = new RefreshToken()
            {
                CreatedAt = DateTime.Now,
                IsActive = true,
                IsRevoked = false,
                UserId = user.Id,
                Value = refreshTokenValue
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshTokenValue;
        }

        private string RandomStringGeneration(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz_!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
