﻿using Market.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Market.API.Data
{
    //This represents our Data Layer of our N layer architecture implementation structure.
    //This class will populate the mongodb at application startup.
    public class MarketContextSeed
    {
        //This method will seed the monngodb with the preconfigured products.
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            //Check for existing product collection and insert data if no data exists.
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct) 
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        //This method provides the preconfigured seeding data.
        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Name",
                    Category = "Market Product",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-1.png",
                    Price = 999.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Name",
                    Category = "Market Product",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-2.png",
                    Price = 999.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Name",
                    Category = "Market Product",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-3.png",
                    Price = 999.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Name = "Name",
                    Category = "Barter Product",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-4.png",
                    Price = 999.00M
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Name = "Name",
                    Category = "Giveaway Product",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-5.png",
                    Price = 999.00M
                }
            };
        }
    }
}
