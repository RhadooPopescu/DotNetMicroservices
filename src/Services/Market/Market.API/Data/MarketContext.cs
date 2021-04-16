using Market.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Market.API.Data
{
    public class MarketContext : IMarketContext
    {
        public MarketContext(IConfiguration configuration)
        {
            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            IMongoDatabase database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            MarketContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
