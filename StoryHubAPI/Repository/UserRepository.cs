using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoryHubAPI.Data;
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

        public async Task<bool> IsUniqueUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            return user is null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequestDTO.Username);

            if (user is null)
            {
                return new LoginResponseDTO()
                {
                    AccessToken = "",
                    User = null
                };
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValid)
            {
                return new LoginResponseDTO()
                {
                    AccessToken = "",
                    User = null
                };
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
                    Username = user.UserName
                }
            };
            
        }

        public async Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO)
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
            return new UserDTO();
        }

        public async Task<string> Refresh(string accessToken, string refreshToken)
        {
            string userId = _accessTokenService.ReadUserId(accessToken);

            User user = new User() { Id = userId };

            if (!await _refreshTokenService.ValidateRefreshTokenAsync(user, refreshToken))
            { 
                throw new Exception("Ivalid refresh token.");  
            }

            return await _accessTokenService.GenerateJwtTokenAsync(user);
        }

        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null) return false;

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            await _refreshTokenService.RevokeRefreshTokenAsync(user);

            return result.Succeeded;
        }
    }
}
