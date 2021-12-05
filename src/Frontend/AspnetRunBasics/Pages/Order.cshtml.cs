using AspnetRunBasics.Contracts;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class OrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderModel(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public IEnumerable<OrderResponseViewModel> Orders { get; set; } = new List<OrderResponseViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["UserLoginCookie"];
            var userName = Request.Cookies["UserName"];

            if(token == null)
            {
                return RedirectToPage("/Login");
            }

            Orders = await _orderService.GetOrdersByUserName(userName, token);
            return Page();
        }
    }
}