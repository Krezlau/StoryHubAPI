using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Data;
using StoryHubAPI.Models;

namespace StoryHubAPI.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly StoryHubDbContext _context;

        public RefreshTokenService(StoryHubDbContext context)
        {
            _context = context;
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

        public async Task RevokeRefreshTokenAsync(User user)
        {
            var dbToken = await RetrieveUserRefreshTokenAsync(user);
            if (dbToken is null) return;
            dbToken.IsRevoked = true;
            dbToken.IsActive = false;
            _context.RefreshTokens.Update(dbToken);
            await _context.SaveChangesAsync();
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
