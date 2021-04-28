using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    //Using Microsoft AspNetCore.MVC Attributes, we are converting this class into an ApiController that inherits from the ControllerBase class.
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        //Injecting the IBasketRepository,
        private readonly IBasketRepository repository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        //Constructor.
        public BasketController(IBasketRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }




        //The following 3 methods should be implemented with async and await and use the Microsoft AspNetCore.MVC Attributes.
        //Get basket method.
        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> GetBasket(string userName)
        {
            ShoppingBasket basket = await repository.GetBasket(userName);
            //if the basket value is null from redis database, we create a basket with a given username for the user.
            return Ok(basket ?? new ShoppingBasket(userName));
        }

        //Update basket method.
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> UpdateBasket([FromBody] ShoppingBasket basket)
        {
            return Ok(await repository.UpdateBasket(basket));
        }

        //Delete basket method for a given username.
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return Ok();
        }


        //This method checks out the basket with the products and total price.
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            string basketCheckoutUserName = basketCheckout.UserName;
            //get existing basket for the given username.
            ShoppingBasket basket = await repository.GetBasket(basketCheckoutUserName);
            if (basket == null)
            {
                return BadRequest();
            }

            //set TotalPrice on basketCheckout eventMessage.
            BasketCheckoutEvent eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            //send checkout event to rabbitmq.
            await publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            //remove the basket.
            string basketUserName = basket.UserName;
            await repository.DeleteBasket(basketUserName);

            return Accepted();
        }
    }
}
