using Basket.Domain.Entities;
using System.Threading.Tasks;

namespace Basket.Domain.Contracts.Services
{
    public interface IBasketService : IServiceBase<ShoppingCart>
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
        Task DeleteBasket(string userName);
    }
}
