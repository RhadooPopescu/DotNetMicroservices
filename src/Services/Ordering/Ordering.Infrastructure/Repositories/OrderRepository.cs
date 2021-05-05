using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    //This class handles database related actions.
    //This class retrieves the Order entity from the Ordering.Domain and handles the Order information for a given username.
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        //Constructor with passing the OrderContext object into the base operation.
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        //This method will get orders by filtering the username.
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            List<Order> orderList = await dbContext.Orders
                                .Where(o => o.UserName == userName)
                                .ToListAsync();
            return orderList;
        }
    }
}
