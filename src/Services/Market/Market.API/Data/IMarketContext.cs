using Market.API.Entities;
using MongoDB.Driver;

namespace Market.API.Data
{
    //This represents our Data Layer of our N layer architecture implementation structure.
    //This interface stores the Product collection of mongodb.
    public interface IMarketContext
    {
        //Defining the property under the IMarketContext interface, meaning that any class that
        //will inherit or implement from this interface will provide a product collection which retuns the IMongoCollection. 
        IMongoCollection<Product> Products { get; }
    }
}
