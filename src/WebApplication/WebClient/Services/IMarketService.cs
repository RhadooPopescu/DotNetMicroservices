using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    //This interface will perform the cosume operations for the Market microservices.
    public interface IMarketService
    {
        //This method will get market product list, and cosume the 
        //product list into the IEnumerable MarketModel.
        Task<IEnumerable<MarketModel>> GetMarket();

        //This method will get market product list by category, and cosume the 
        //product list into the IEnumerable MarketModel.
        Task<IEnumerable<MarketModel>> GetMarketByCategory(string category);

        //This method will get market product list by id, and cosume the 
        //product list into the MarketModel.
        Task<MarketModel> GetMarket(string id);

        Task<MarketModel> CreateMarket(MarketModel model);
    }
}
