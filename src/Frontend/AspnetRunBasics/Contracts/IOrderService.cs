using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseViewModel>> GetOrdersByUserName(string userName);
        Task<IEnumerable<OrderResponseViewModel>> GetOrdersByUserName(string userName, string token);
    }
}
