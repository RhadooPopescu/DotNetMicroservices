using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
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
            HttpResponseMessage response = await client.GetAsync("/api/v1/Market");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> GetMarket(string id)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/v1/Market/{id}");
            return await response.ReadContentAs<MarketModel>();
        }

        public async Task<IEnumerable<MarketModel>> GetMarketByCategory(string category)
        {
            HttpResponseMessage response = await client.GetAsync($"/api/v1/Market/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<MarketModel>>();
        }
    }
}
