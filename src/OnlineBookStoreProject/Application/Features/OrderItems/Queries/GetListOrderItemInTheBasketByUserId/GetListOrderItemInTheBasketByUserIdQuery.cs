using Core.Application.Requests;
using Application.Features.OrderItems.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderItems.Queries.GetListOrderItemInTheBasketByUserId
{
    public class GetListOrderItemInTheBasketByUserIdQuery: IRequest<OrderItemListModel>
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }
        

        public class GetListOrderItemInTheBasketByUserIdQueryHandler :IRequestHandler<GetListOrderItemInTheBasketByUserIdQuery,OrderItemListModel>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;

            public GetListOrderItemInTheBasketByUserIdQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }

            public async Task<OrderItemListModel> Handle(GetListOrderItemInTheBasketByUserIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderItem> orderItems = await _orderItemRepository.GetListAsync(index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize, include: x => x.Include(x => x.User), 
                    predicate: x=>x.IsInTheBasket==true&&x.UserId==request.UserId);

                OrderItemListModel orderItemListModel = _mapper.Map<OrderItemListModel>(orderItems);

                return orderItemListModel;


            }


        }


    }
}
