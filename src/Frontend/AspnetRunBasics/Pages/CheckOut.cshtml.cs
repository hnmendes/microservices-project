using AspnetRunBasics.Contracts;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public CheckOutModel(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [BindProperty]
        public BasketCheckoutViewModel Order { get; set; }

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

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            var token = Request.Cookies["UserLoginCookie"];
            var userName = Request.Cookies["UserName"];

            if (token == null)
            {
                return LocalRedirect("/Login");
            }

            Cart = await _basketService.GetBasket(userName, token);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}