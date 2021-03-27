using Market.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.API.Data
{
    public interface IMarketContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
