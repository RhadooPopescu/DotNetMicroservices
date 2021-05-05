using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    //This class handles the order delete.
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        //Fields.
        //IOrderRepository required for inserting the record, IMapper for mapping the comand handler to the entity objects,
        //and ILogger to log the performed operations.
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteOrderCommandHandler> logger;

        //Constructor.
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //This method handles the delete opretations of the order for a given order id, logs the order information,
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            int requestId = request.Id;

            //Getting the order record from the database
            Order orderToDelete = await orderRepository.GetByIdAsync(requestId);
            int orderToDeleteId = orderToDelete.Id;
            //If order is not found trow exception error
            if (orderToDelete == null)
            {
                throw new NotFoundException(nameof(Order), requestId);
            }
            //Delete the selected order
            await orderRepository.DeleteAsync(orderToDelete);
            logger.LogInformation($"Order {orderToDeleteId} was successfully deleted.");

            return Unit.Value;
        }
    }
}
