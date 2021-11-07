using Basket.Domain.Entities;
using System.Threading.Tasks;

namespace Basket.Domain.Contracts.Repositories
{
    public interface IBasketRepository : IRepositoryBase<ShoppingCart>
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
        Task DeleteBasket(string userName);
    }
}
