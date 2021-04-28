using Market.API.Data;
using Market.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.API.Repositories
{
    //This represents our Business Layer of our N layer architecture implementation structure.
    //The ProductRepository class implements from the IProductRepository interface.
    //The methods in this class are the same as mongodb cli commands.
    public class ProductRepository : IProductRepository
    {
        //Injecting the IMarketContext.
        private readonly IMarketContext context;

        //Constructor.
        public ProductRepository(IMarketContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //This method returns the products from the mongodb.
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context                    //using the context object for performing database operations.
                            .Products               //product collection under the market context.
                            .Find(p => true)        //mongo cli Find command. 
                            .ToListAsync();         //return the ToListAsync in order to perform await and async method.
        }

        //This method returns the products from mongodb by id, using the id as a parameter.
        public async Task<Product> GetProduct(string id)
        {
            return await context
                           .Products
                           .Find(p => p.Id == id)    //providing the criteria filter information.
                           .FirstOrDefaultAsync();   
        }

        //This method returns the product from mongodb by name, using the name as a parameter.
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            //creating the filter operation by name using Filter.Eq method from mongodb driver.
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await context
                            .Products
                            .Find(filter)     //passing the filter in the Find method in order to find the products by name.
                            .ToListAsync();
        }

        //This method returns the product from mongodb by category using the category name as a parameter.
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            //creating the filter operation by categoryName using Filter.Eq method from mongodb driver.
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await context
                            .Products
                            .Find(filter)    //passing the filter in the Find method in order to find the products by category.
                            .ToListAsync();
        }

        //Performing crud operations
        //This method uses the product information as a parameter and creates a new product.
        public async Task CreateProduct(Product product)
        {
            await context.Products.InsertOneAsync(product);
        }

        //This method updates an already existing product, in accordance with the id of the product.
        public async Task<bool> UpdateProduct(Product product)
        {
            ReplaceOneResult updateResult = await context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product); //filter the id and replace if id match.

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        //This method deletes an existing product, in accordance with the id of the product.
        public async Task<bool> DeleteProduct(string id)
        {
            //creating a filter operation by id using the Filter.Eq method from mongodb driver.
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
