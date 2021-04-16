using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
