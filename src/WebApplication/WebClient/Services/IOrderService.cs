using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    //This interface will perform the cosume operations for the Ordering microservices.
    public interface IOrderService
    {
        //This method will get orders for the given the username, 
        //and cosume the orders list into the IEnumerable OrderResponseModel.
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
