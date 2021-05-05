using AutoMapper;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mappings
{
    //This class is mapping Ordering.Domain objects with Ordering.Application objects using AutoMapper.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //The Order object will be mapped from the OrdesVm object.
            CreateMap<Order, OrdersVm>().ReverseMap();
            //The Order object will be mapped from the CheckoutOrderCommand object.
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            //The Order object will be mapped from the UpdateOrderCommand object.
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
