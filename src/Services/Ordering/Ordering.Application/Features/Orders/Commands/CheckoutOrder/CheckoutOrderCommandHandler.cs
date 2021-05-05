using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    //This class handles the order checkout with sending email to the customer.
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        //Fields.
        //IOrderRepository required for inserting the record, IMapper for mapping the comand handler to the entity objects,
        //IEmailService for sending email, and ILogger to log the performed operations.
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> logger;

        //Constructor.
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //This method handles the checkout opretations of the order, logs the order information and sends an email to the customer.
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            Order orderEntity = mapper.Map<Order>(request);
            Order newOrder = await orderRepository.AddAsync(orderEntity);

            logger.LogInformation($"Order {newOrder.Id} was successfully created.");
            await SendMail(newOrder);

            return newOrder.Id;
        }

        //This method sends a confirmation email to the customer. Currently this method is for testing purposes.
        private async Task SendMail(Order order)
        {
            Email email = new Email() { To = "radu.popesku@gmail.com", Body = $"Order details.", Subject = "Your order was successfully created" };

            try
            {
                await emailService.SendEmail(email);
            }
            catch (Exception e)
            {
                logger.LogError($"Email sending for order {order.Id} failed due to: {e.Message}");
            }
        }
    }
}
