using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    //This interface will perform the cosume operations for the Market microservices.
    public interface IMarketService
    {
        //This method will get market product list, and store the 
        //product list into the IEnumerable MarketModel.
        Task<IEnumerable<MarketModel>> GetMarket();
        //This method will get market product list by category, and store the 
        //product list into the IEnumerable MarketModel.
        Task<IEnumerable<MarketModel>> GetMarketByCategory(string category);
        //This method will get market product list by id, and store the 
        //product list into the MarketModel.
        Task<MarketModel> GetMarket(string id);
    }
}
