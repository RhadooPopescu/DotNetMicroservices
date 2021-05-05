using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    //This interface will perform the cosume operations for the Ordering microservices.
    public interface IOrderService
    {
        //This method will get orders for the given the username, 
        //and store the orders list into the IEnumerable OrderResponseModel.
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
