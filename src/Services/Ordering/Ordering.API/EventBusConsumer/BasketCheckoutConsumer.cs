﻿using AutoMapper;
using EventBus.Messages.Events;
using MediatR;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumer
{
    //This class consumes the basket checkout event with Masstransit RabbitMq.
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        //IMapper for mapping BasketCheckoutEvent objects with BasketCheckoutConsumer,
        //IMediator for performing cosume operations, and ILogger for logging the BasketCheckoutConsumer events.
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ILogger<BasketCheckoutConsumer> logger;

        //Constructor.
        public BasketCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //This method subscribes basket checkout MassTransit que message event in the Ordering.API.
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            BasketCheckoutEvent contextMessage = context.Message;

            //creating a mapper command object and sendind the command to the mediator.
            CheckoutOrderCommand command = mapper.Map<CheckoutOrderCommand>(contextMessage);
            int result = await mediator.Send(command);

            logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id : {newOrderId}", result);
        }
    }
}
