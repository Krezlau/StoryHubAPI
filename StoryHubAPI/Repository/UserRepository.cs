using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using System.Runtime.CompilerServices;

namespace StoryHubAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StoryHubDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey = "secret"; // for now

        public UserRepository(StoryHubDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Token = "",
                    User = null
                };
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValid)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            // here gonna need to generate jwt token

            return new LoginResponseDTO()
            {
                Token = "token",
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
                return new UserDTO()
                {
                    Id = user.Id,
                    Username = registerRequestDTO.Username
                };
            }
            return new UserDTO();
        }
    }
}
