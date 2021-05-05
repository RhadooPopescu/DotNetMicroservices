using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Extensions;
using WebClient.Models;

namespace WebClient.Services
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
        //and cosume the basket list into the BasketModel.
        public async Task<BasketModel> GetBasket(string userName)
        {
            HttpResponseMessage response = await client.GetAsync($"/Basket/{userName}");
            return await response.ReadContentAs<BasketModel>();
        }

        //This method will update the shopping basket, 
        //and cosume the basket list into the BasketModel.
        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            HttpResponseMessage response = await client.PostAsJson($"/Basket", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<BasketModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        //This method will checkout the shopping basket, 
        //according to the BasketCheckoutModel.
        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            HttpResponseMessage response = await client.PostAsJson($"/Basket/Checkout", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
