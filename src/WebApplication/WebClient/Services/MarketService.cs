using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Extensions;
using WebClient.Models;

namespace WebClient.Services
{
    //This class implements the IMarketService methods with the http client object.
    //This approach is known as: type based http client factory registration.
    public class MarketService : IMarketService
    {
        //Injecting the http client object.
        private readonly HttpClient client;

        //Constructor.
        public MarketService(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        //This method will get market product list, and cosume the 
        //product list into the IEnumerable MarketModel.
        public async Task<IEnumerable<MarketModel>> GetMarket()
        {
            HttpResponseMessage response = await client.GetAsync("/Market");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        //This method will get market product list by id, and cosume the 
        //product list into the MarketModel.
        public async Task<MarketModel> GetMarket(string id)
        {
            HttpResponseMessage response = await client.GetAsync($"/Market/{id}");
            return await response.ReadContentAs<MarketModel>();
        }

        //This method will get market product list by category, and cosume the 
        //product list into the IEnumerable MarketModel.
        public async Task<IEnumerable<MarketModel>> GetMarketByCategory(string category)
        {
            HttpResponseMessage response = await client.GetAsync($"/Market/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> CreateMarket(MarketModel model)
        {
            HttpResponseMessage response = await client.PostAsJson($"/Market", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<MarketModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
