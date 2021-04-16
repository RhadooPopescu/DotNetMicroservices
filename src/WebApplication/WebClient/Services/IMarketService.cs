using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    public interface IMarketService
    {
        Task<IEnumerable<MarketModel>> GetMarket();
        Task<IEnumerable<MarketModel>> GetMarketByCategory(string category);
        Task<MarketModel> GetMarket(string id);
        Task<MarketModel> CreateMarket(MarketModel model);
    }
}
