using Market.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Market.API.Data
{
    //This represents our Data Layer of our N layer architecture implementation structure.
    //This class will create the connection with the mongodb.
    public class MarketContext : IMarketContext
    {
        //Constructor.
        public MarketContext(IConfiguration configuration)
        {
            //Creating the connection string with mongodb using the MongoClient through the configuration object.
            MongoClient client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Creating the database and getting the database.
            IMongoDatabase database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            //Populate the Product collection with using the database object.
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            //Seeding the Products into the database.
            MarketContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
