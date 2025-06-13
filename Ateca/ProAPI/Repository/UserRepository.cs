using RestAPI.Data;
using RestAPI.Models.DTOs.UserDto;
using RestAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using RestAPI.Models.Entity;

namespace RestAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly string secretKey;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly int TokenExpirationDays = 7;

        public UserRepository(ApplicationDbContext context, IConfiguration config,
            UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            secretKey = config.GetValue<string>("ApiSettings:SecretKey");
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public AppUser GetUser(string id)
        {
            return _context.AppUsers.FirstOrDefault(user => user.Id == id);
        }

        public ICollection<AppUser> GetUsers()
        {
            return _context.AppUsers.OrderBy(user => user.UserName).ToList();
        }

        public bool IsUniqueUser(string userName)
        {
            return !_context.AppUsers.Any(user => user.UserName == userName);
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                return new UserLoginResponseDto { Token = "", User = null };
            }
            bool isValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            //user doesn't exist ?
            if (user == null || !isValid)
            {
                return new UserLoginResponseDto { Token = "", User = null };
            }

            //User does exist
            var roles = await _userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
         
            if (secretKey.Length < 32)
            {
                throw new ArgumentException("The secret key must be at least 32 characters long.");
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(TokenExpirationDays),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponseDto userLoginResponseDto = new UserLoginResponseDto
            {
                Token = tokenHandler.WriteToken(jwtToken),
                User = user
            };
            return userLoginResponseDto;
        }

        private string GenerateJwtToken(AppUser user, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(TokenExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<UserLoginResponseDto?> Register(UserRegistrationDto userRegistrationDto)
        {
            AppUser user = new AppUser
            {
                UserName = userRegistrationDto.Name,
                Name = userRegistrationDto.Name,
                Email = userRegistrationDto.Email,
                NormalizedEmail = userRegistrationDto.Email.ToUpper()
            };

            var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);
            if (!result.Succeeded)
            {
                return null;
            }

            // Releer el usuario para asegurar que tenga ID válido
            user = await _userManager.FindByEmailAsync(user.Email);

            // Crear el rol si no existe
            if (!await _roleManager.RoleExistsAsync(userRegistrationDto.Role))
            {
                var role = new IdentityRole
                {
                    Name = userRegistrationDto.Role,
                    NormalizedName = userRegistrationDto.Role.ToUpper()
                };

                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Role creation failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }

            // Asignar rol al usuario
            var addRoleResult = await _userManager.AddToRoleAsync(user, userRegistrationDto.Role);
            if (!addRoleResult.Succeeded)
            {
                throw new Exception($"Adding role failed: {string.Join(", ", addRoleResult.Errors.Select(e => e.Description))}");
            }

            // Verificar roles asignados
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles);

            return new UserLoginResponseDto
            {
                Token = token,
                User = user
            };
        }


    }
}
