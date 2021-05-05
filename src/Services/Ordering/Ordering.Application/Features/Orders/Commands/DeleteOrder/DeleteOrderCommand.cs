using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    //This class implements the definition for delete order command handler, which requires the order id to be performed.
    public class DeleteOrderCommand : IRequest
    {
        //Property for deleting an order.
        public int Id { get; set; }
    }
}
