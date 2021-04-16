using AutoMapper;
using EventBus.Messages.Events;
using MediatR;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumer
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ILogger<BasketCheckoutConsumer> logger;

        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            CheckoutOrderCommand command = mapper.Map<CheckoutOrderCommand>(context.Message);
            int result = await mediator.Send(command);

            logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
