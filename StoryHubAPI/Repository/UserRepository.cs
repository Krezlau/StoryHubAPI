using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoryHubAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StoryHubDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey = "extremelySecretKey"; // for now

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

            string token = await CreateToken(user);

            return new LoginResponseDTO()
            {
                Token = token,
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

        private async Task<string> CreateToken(User user)
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
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
