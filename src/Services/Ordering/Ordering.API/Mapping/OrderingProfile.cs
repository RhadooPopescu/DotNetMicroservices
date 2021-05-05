using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.Mapping
{
    //This class is mapping between CheckoutOrderCommand objects and BasketCheckoutEvent
    //in order to apply operations in the CQRS design pattern Ordering.Application.
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
