using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Api.Extensions;
using User.Application.Models.Requests;
using User.Application.Services.Contracts;
using User.Domain.Entities.Enums.User;

namespace User.Api.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetUsers());
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            return Ok(await _userService.Login(loginRequest));
        }

        [AllowAnonymous]
        [HttpPost("/sign-up")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
            {
                return BadRequest("You must fill the form.");
            }

            return Ok(await _userService.Register(registerRequest));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/add-user-role/{userName}/{role}")]
        public async Task<ActionResult> AddUserRole(string userName, string role)
        {
            return Ok(await _userService.AddUserRole(role, userName));
        }
    }
}
