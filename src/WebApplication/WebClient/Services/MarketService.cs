using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Extensions;
using WebClient.Models;

namespace WebClient.Services
{
    public class MarketService : IMarketService
    {
        private readonly HttpClient client;

        public MarketService(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<MarketModel>> GetMarket()
        {
            HttpResponseMessage response = await client.GetAsync("/Market");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> GetMarket(string id)
        {
            HttpResponseMessage response = await client.GetAsync($"/Market/{id}");
            return await response.ReadContentAs<MarketModel>();
        }

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
