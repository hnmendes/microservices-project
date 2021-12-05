using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Contracts
{
    public interface IUserService
    {
        Task<LoginResponseViewModel> Login(LoginRequestViewModel login);
        Task<bool> Register(RegisterRequestViewModel register);
    }
}
