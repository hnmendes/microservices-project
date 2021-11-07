using Basket.Domain.Contracts.Repositories;
using Basket.Domain.Contracts.Services;
using Basket.Domain.Entities;
using System.Threading.Tasks;

namespace Basket.Domain.Services
{
    public class BasketService : ServiceBase<ShoppingCart>, IBasketService
    {
        private readonly IBasketRepository _repo;

        public BasketService(IBasketRepository repo) : base(repo)
        {
            _repo = repo;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            return await _repo.GetBasket(userName);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            return await _repo.UpdateBasket(cart);
        }

        public async Task DeleteBasket(string userName)
        {
            await _repo.DeleteBasket(userName);
        }
    }
}
