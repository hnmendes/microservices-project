using AspnetRunBasics.Models;
using System.Threading.Tasks;

namespace AspnetRunBasics.Contracts
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetBasket(string userName);
        Task<BasketViewModel> GetBasket(string userName, string token);
        Task<BasketViewModel> UpdateBasket(BasketViewModel model);
        Task<BasketViewModel> UpdateBasket(BasketViewModel model, string token);
        Task CheckoutBasket(BasketCheckoutViewModel model);
        Task CheckoutBasket(BasketCheckoutViewModel model, string token);
    }
}
