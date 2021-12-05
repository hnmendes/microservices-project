using AspnetRunBasics.Contracts;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }


        public async Task<LoginResponseViewModel> Login(LoginRequestViewModel login)
        {
            var response = await _client.PostAsJson("/user/login", login);
            
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<LoginResponseViewModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task<bool> Register(RegisterRequestViewModel register)
        {
            var response = await _client.PostAsJson("/user/sign-up", register);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
