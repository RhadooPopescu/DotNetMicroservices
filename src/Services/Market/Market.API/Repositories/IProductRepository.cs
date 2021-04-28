using Market.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;


namespace Market.API.Repositories
{
    //This represents our Business Layer of our N layer architecture implementation structure.
    //This interface represents an abstraction layer for the data operations.
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(string id);

        Task<IEnumerable<Product>> GetProductByName(string name);

        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task CreateProduct(Product product);

        Task<bool> UpdateProduct(Product product);

        Task<bool> DeleteProduct(string id);


    }
}
