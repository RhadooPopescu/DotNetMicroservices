using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    //This class handles the order update.
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        //Fields.
        //IOrderRepository required for inserting the record, IMapper for mapping the comand handler to the entity objects,
        //and ILogger to log the performed operations.
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateOrderCommandHandler> logger;

        //Constructor.
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //This method handles the update opretations of the order for a given order id, logs the order information,
        //and returns the newly updated order.
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            int requestId = request.Id;

            //Getting the order record from the database
            Order orderToUpdate = await orderRepository.GetByIdAsync(requestId);
            int orderToUpdateId = orderToUpdate.Id;
            //If order is not found trow exception error
            if (orderToUpdate == null)
            {
                throw new NotFoundException(nameof(Order), requestId);
            }
            //Update the requested order
            mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
            await orderRepository.UpdateAsync(orderToUpdate);

            logger.LogInformation($"Order {orderToUpdateId} was successfully updated.");

            return Unit.Value;
        }
    }
}