﻿using Shopping.Aggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface IMarketService
    {
        Task<IEnumerable<MarketModel>> GetMarket();
        Task<IEnumerable<MarketModel>> GetMarketByCategory(string category);
        Task<MarketModel> GetMarket(string id);
    }
}