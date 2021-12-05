using AspnetRunBasics.Contracts;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public BasketViewModel Cart { get; set; } = new BasketViewModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["UserLoginCookie"];
            var userName = Request.Cookies["UserName"];

            if (token == null)
            {
                return LocalRedirect("/Login");
            }

            Cart = await _basketService.GetBasket(userName, token);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var token = Request.Cookies["UserLoginCookie"];
            var userName = Request.Cookies["UserName"];

            if (token == null)
            {
                return LocalRedirect("/Login");
            }
            var basket = await _basketService.GetBasket(userName, token);

            var item = basket.Items.Single(x => x.ProductId == productId);
            basket.Items.Remove(item);

            var basketUpdated = await _basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}