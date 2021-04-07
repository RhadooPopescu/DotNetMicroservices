using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class MarketService : IMarketService
    {
        private readonly HttpClient _client;

        public MarketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<MarketModel>> GetMarket()
        {
            var response = await _client.GetAsync("/api/v1/Market");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> GetMarket(string id)
        {
            var response = await _client.GetAsync($"/api/v1/Market/{id}");
            return await response.ReadContentAs<MarketModel>();
        }

        public async Task<IEnumerable<MarketModel>> GetMarketByCategory(string category)
        {
            var response = await _client.GetAsync($"/api/v1/Market/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<MarketModel>>();
        }
    }
}
