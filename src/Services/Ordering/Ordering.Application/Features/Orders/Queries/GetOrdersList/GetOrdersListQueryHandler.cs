using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    //This class will be trigered by the MediatR when a request comes from the request object(GetOrderListQuery) and the expected response(List<OrdersVM>).
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        //Fields.
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        //Constructor.
        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //This handle method will return the order list with the requested username included.
        //After getting the order entity list we have to map the orderList to our OrdersVm DTO object.
        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            string requestUserName = request.UserName;

            var orderList = await orderRepository.GetOrdersByUserName(requestUserName);
            return mapper.Map<List<OrdersVm>>(orderList);
        }
    }
}
