using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Application.Models.Requests;
using User.Application.Models.Responses;
using User.Application.Services.Contracts;
using User.Domain.Contracts.Repositories;
using User.Domain.Entities;
using User.Domain.Entities.Enums.User;
using User.Infrastructure.Security;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AddUserRoleResponse> AddUserRole(string role, string userNameOrEmail)
        {
            var user = await _userRepository.GetByUserNameOrEmail(userNameOrEmail);

            if(!HasRole(user, role))
            {
                user.Role = string.Join(',', user.Role, role);
                await _userRepository.UpdateAsync(user);

                return new AddUserRoleResponse()
                {
                    Message = $"The role has been updated for {userNameOrEmail}; Currently Roles: {user.Role}."
                };
            }

            return new AddUserRoleResponse()
            {
                Message = $"The user {userNameOrEmail} already has the role {role}"
            };
        }

        public async Task<IEnumerable<ResponseUser>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var userResponse = new List<ResponseUser>();

            foreach(Domain.Entities.User user in users)
            {
                userResponse.Add(new ResponseUser()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Id = user.Id,
                    LastName = user.LastName,
                    Username = user.UserName
                });
            }

            return userResponse;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUserNameOrEmail(loginRequest.UserName);

            var passwordMatched = PasswordHandle.IsPasswordMatch(loginRequest.Password, user.PasswordHash, user.PasswordSalt);

            if (passwordMatched)
            {
                var token = generateJwtToken(user);
                var loginResponse = new LoginResponse(user, token);
                return loginResponse;
            }

            return null;
        }

        public async Task<bool> Register(RegisterRequest registerRequest)
        {
            try
            {
                var salt = PasswordHandle.CreateSalt(64);
                var passwordHash = PasswordHandle.GenerateHash(registerRequest.Password, salt);

                var user = new Domain.Entities.User()
                {
                    UserName = registerRequest.UserName,
                    Email = registerRequest.Email,
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    Role = ConstantsRole.Roles.Member,
                    PasswordSalt = salt,
                    PasswordHash = passwordHash
                };

                await _userRepository.CreateAsync(user);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private bool HasRole(Domain.Entities.User user, string role)
        {
            if (role == null) return false;

            if(user.Role.Contains(role)) return true;

            return false;
        }

        private string generateJwtToken(Domain.Entities.User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AuthSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, ConstantsRole.Roles.Member),
                    new Claim(ClaimTypes.Role, ConstantsRole.Roles.Admin),
                    new Claim(ClaimTypes.Role, ConstantsRole.Roles.Salesman),
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
