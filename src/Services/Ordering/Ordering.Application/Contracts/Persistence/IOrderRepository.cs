using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    //This interface handles database related actions.
    //This interface retrieves the Order entity from the Ordering.Domain and handles the Order information.
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        //This is our custom query method for the Order entity that retrieves the Order for the given username.
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
