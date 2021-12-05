using AspnetRunBasics.Contracts;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OrderResponseViewModel>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/order/{userName}");
            return await response.ReadContentAs<List<OrderResponseViewModel>>();
        }

        public async Task<IEnumerable<OrderResponseViewModel>> GetOrdersByUserName(string userName, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _client.GetAsync($"/order/{userName}");
            return await response.ReadContentAs<List<OrderResponseViewModel>>();
        }
    }
}
