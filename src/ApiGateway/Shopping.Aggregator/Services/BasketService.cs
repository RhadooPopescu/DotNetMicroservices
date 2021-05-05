using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    //This class implements the IBasketService methods with the http client object.
    //This approach is known as: type based http client factory registration.
    public class BasketService : IBasketService
    {
        //Injecting the http client object.
        private readonly HttpClient client;

        //Constructor.
        public BasketService(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        //This method will get the shopping basket for the given the username, 
        //and store the basket list into the BasketModel.
        public async Task<BasketModel> GetBasket(string userName)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/v1/Basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
