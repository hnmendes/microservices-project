using User.Application.Models.Requests;
using User.Application.Models.Responses;

namespace User.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<bool> Register(RegisterRequest registerRequest);
        Task<IEnumerable<ResponseUser>> GetUsers();
        Task<AddUserRoleResponse> AddUserRole(string role, string userNameOrEmail);
    }
}
