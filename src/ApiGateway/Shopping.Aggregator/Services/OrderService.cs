using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    //This class implements the IOrderService methods with the http client object.
    //This approach is known as: type based http client factory registration.
    public class OrderService : IOrderService
    {
        //Injecting the http client object.
        private readonly HttpClient client;

        //Constructor.
        public OrderService(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        //This method will get orders for the given the username, 
        //and store the orders list into the IEnumerable OrderResponseModel.
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/v1/Order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
