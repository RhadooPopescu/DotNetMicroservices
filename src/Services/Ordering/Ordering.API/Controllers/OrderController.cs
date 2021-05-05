using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    //This represents our Presentation Layer of our CQRS and Clean Architecture implementation structure.
    //Using Microsoft AspNetCore.MVC Attributes, we are converting this class into an ApiController that inherits from the ControllerBase class.
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        //Fields, we only need the mediator to perform all the operations.
        private readonly IMediator mediator;

        //Constructor.
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //This query method returns the order for the provided username.
        [HttpGet("{userName}", Name = "GetOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUserName(string userName)
        {
            GetOrdersListQuery query = new GetOrdersListQuery(userName);
            List<OrdersVm> orders = await mediator.Send(query);
            return Ok(orders);
        }

        //This method handles the checkout order command. This method is implemented for testing purpose.
        //The actual checkout order command will be triggered from RabbitMq implementation.
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            int result = await mediator.Send(command);
            return Ok(result);
        }

        //This method handles the update order command.
        [HttpPut(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        //This method handles the delete order command.
        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            DeleteOrderCommand command = new DeleteOrderCommand() { Id = id };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
