using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Extensions;
using WebClient.Models;

namespace WebClient.Services
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
            var response = await _client.GetAsync("/Market");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> GetMarket(string id)
        {
            var response = await _client.GetAsync($"/Market/{id}");
            return await response.ReadContentAs<MarketModel>();
        }

        public async Task<IEnumerable<MarketModel>> GetMarketByCategory(string category)
        {
            var response = await _client.GetAsync($"/Market/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<MarketModel>>();
        }

        public async Task<MarketModel> CreateMarket(MarketModel model)
        {
            var response = await _client.PostAsJson($"/Market", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<MarketModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
