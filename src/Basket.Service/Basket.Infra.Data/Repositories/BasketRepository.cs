using Basket.Domain.Contracts.Repositories;
using Basket.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.Infra.Data.Repositories
{
    public class BasketRepository : RepositoryBase<ShoppingCart>, IBasketRepository
    {
        public BasketRepository(IDistributedCache distributedCache) : base(distributedCache)
        {

        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var cart = await Get(userName);

            if (cart is null) return null;

            return JsonSerializer.Deserialize<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            await _distributedCache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));

            return await GetBasket(cart.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await Delete(userName);
        }
    }
}
