using Market.API.Entities;
using MongoDB.Driver;

namespace Market.API.Data
{
    public interface IMarketContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
