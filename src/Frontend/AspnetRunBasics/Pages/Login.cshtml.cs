using AspnetRunBasics.Contracts;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public void OnGet()
        {
        }

        [BindProperty()]
        public LoginRequestViewModel LoginViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.Login(LoginViewModel);

            if (result is null)
            {
                ModelState.Clear();
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "There's no User Name or Email");
                ModelState.AddModelError(nameof(LoginViewModel.Password), "There's no password");
                return Page();
            }
            Response.Cookies.Append("UserName", $"{result.Username}");
            Response.Cookies.Append("UserEmail", $"{result.Email}");
            Response.Cookies.Append("UserLoginCookie", $"Bearer {result.Token}");            

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            Response.Cookies.Delete("UserLoginCookie");
            return RedirectToPage("/Login");
        }
    }
}
