using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoryHubAPI.Data;
using StoryHubAPI.Exceptions;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace StoryHubAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StoryHubDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserRepository(StoryHubDbContext context,
                              UserManager<User> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IAccessTokenService tokenService,
                              IRefreshTokenService refreshTokenService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _accessTokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<bool> IsUniqueUserAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return user is null;
        }

        public async Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequestDTO.Username);

            if (user is null)
            {
                throw new AuthException("Incorrect username.");
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValid)
            {
                throw new AuthException("Incorrect password.");
            }

            string token = await _accessTokenService.GenerateJwtTokenAsync(user);
            string refreshToken = await _refreshTokenService.RetrieveOrGenerateRefreshTokenAsync(user);


            return new LoginResponseDTO()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                User = new UserDTO()
                {
                    Id = user.Id,
                    Username = user.UserName!
                }
            };
            
        }

        public async Task<UserDTO> RegisterUserAsync(RegisterRequestDTO registerRequestDTO)
        {
            User user = new User()
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Email,
                NormalizedEmail = registerRequestDTO.Email.ToUpper(),
            };

            var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);

            if (result.Succeeded)
            {
                if (!(await _roleManager.RoleExistsAsync("user")))
                {
                    await _roleManager.CreateAsync(new IdentityRole("user"));
                }

                await _userManager.AddToRoleAsync(user, "user");
                return new UserDTO()
                {
                    Id = user.Id,
                    Username = registerRequestDTO.Username
                };
            }
            else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error.Description);
                }
                throw new AuthException(errors.ToString().TrimEnd());
            }
        }

        public async Task<string> RefreshAsync(string accessToken, string refreshToken)
        {
            string userId;
            try
            {
                userId = _accessTokenService.ReadUserId(accessToken);
            }
            catch (Exception ex)
            {
                throw new AuthException("Invalid access token.", ex);
            }

            User user = new User() { Id = userId };

            if (!await _refreshTokenService.ValidateRefreshTokenAsync(user, refreshToken))
            { 
                throw new AuthException("Ivalid refresh token.");  
            }

            return await _accessTokenService.GenerateJwtTokenAsync(user);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null) return false;

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                await _refreshTokenService.RevokeRefreshTokenAsync(user);
                return true;
            } else
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error.Description);
                }
                throw new AuthException(errors.ToString().TrimEnd());
            }
        }
    }
}
