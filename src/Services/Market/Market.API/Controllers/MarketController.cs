using Market.API.Entities;
using Market.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Market.API.Controllers
{
    //This represents our Presentation Layer of our N layer architecture implementation structure.
    //Using Microsoft AspNetCore.MVC Attributes, we are converting this class into an ApiController that inherits from the ControllerBase class.
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MarketController : ControllerBase
    {
        //Injecting the business layer IProductRepository and the ILogger in order to log our operations.
        private readonly IProductRepository repository;
        private readonly ILogger<MarketController> logger;

        //Constructor.
        public MarketController(IProductRepository repository, ILogger<MarketController> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //The following 3 methods should be implemented with async and await and use the Microsoft AspNetCore.MVC Attributes.
        //Get products method.
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await repository.GetProducts();
            return Ok(products);
        }

        //Get product by id method, with the required length of the mongodb BsonId.
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            Product product = await repository.GetProduct(id);

            if (product == null)
            {
                logger.LogError($"Product with id: {id}, was not found.");
                return NotFound();
            }

            return Ok(product);
        }

        //Get product by category method using the category name as a parameter.
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await repository.GetProductByCategory(category);
            return Ok(products);
        }

        //The following 3 methods for performing crud operations should be implemented with async and await and use the Microsoft AspNetCore.MVC Attributes.
        //Create product method.
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await repository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        //Update product method, using IActionResult as no specific response is required from the result, just returning the 200 success response.
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await repository.UpdateProduct(product));
        }

        //Delete product by id method, using IActionResult as no specific response is required from the result, just returning the 200 success response.
        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await repository.DeleteProduct(id));
        }
    }
}
