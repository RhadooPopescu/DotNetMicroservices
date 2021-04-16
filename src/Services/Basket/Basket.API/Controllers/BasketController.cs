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
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        //fields
        private readonly IBasketRepository repository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndpoint;

        //constructor
        public BasketController(IBasketRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }




        //get basket method
        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> GetBasket(string userName)
        {
            ShoppingBasket basket = await repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingBasket(userName));
        }

        //update basket method
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> UpdateBasket([FromBody] ShoppingBasket basket)
        {
            return Ok(await repository.UpdateBasket(basket));
        }

        //delete basket method
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return Ok();
        }



        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // get existing basket with total price            
            // Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            ShoppingBasket basket = await repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            BasketCheckoutEvent eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish<BasketCheckoutEvent>(eventMessage);

            // remove the basket
            await repository.DeleteBasket(basket.UserName);

            return Accepted();
        }
    }
}
