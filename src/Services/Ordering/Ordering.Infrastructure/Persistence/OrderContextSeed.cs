using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    //This class will seed prebuild data in the orderdb.
    public class OrderContextSeed
    {
        //This method will populate the database with the prebuild data.
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        //This method is providing the prebuild data for orderdb.
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "rdu", TotalPrice = 333, FirstName = "Radu", LastName = "Popescu", 
                    EmailAddress = "rhadoopopescu@gmail.com", AddressLine = "1 Lea Road", Country = "United Kingdom", 
                    State = "London", ZipCode = "LU1 3GG", CardName = "Radu Popescu", CardNumber = "4536728909765242", 
                    Expiration = "08/26", CVV = "767" }
            };
        }
    }
}
