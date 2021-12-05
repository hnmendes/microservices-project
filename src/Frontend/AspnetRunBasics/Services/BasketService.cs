using AspnetRunBasics.Contracts;
using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BasketViewModel> GetBasket(string userName)
        {
            var response = await _client.GetAsync($"/basket/{userName}");
            return await response.ReadContentAs<BasketViewModel>();
        }

        public async Task<BasketViewModel> UpdateBasket(BasketViewModel model)
        {
            var response = await _client.PostAsJson($"/basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketViewModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task CheckoutBasket(BasketCheckoutViewModel model)
        {
            var response = await _client.PostAsJson($"/basket/checkout", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task<BasketViewModel> GetBasket(string userName, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _client.GetAsync($"/basket/{userName}");
            return await response.ReadContentAs<BasketViewModel>();
        }

        public async Task<BasketViewModel> UpdateBasket(BasketViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _client.PostAsJson($"/basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketViewModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task CheckoutBasket(BasketCheckoutViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", token);
            var response = await _client.PostAsJson($"/basket/checkout", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
