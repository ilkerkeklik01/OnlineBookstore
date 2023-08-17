using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderItems.Queries.GetListOrderItemAddedButNotPurchasedByUserId
{
    public class GetListOrderItemAddedButNotPurchasedByUserIdQuery : IRequest<OrderItemListModel>
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListOrderItemAddedButNotPurchasedByUserIdQueryHandler : IRequestHandler<GetListOrderItemAddedButNotPurchasedByUserIdQuery,OrderItemListModel>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;

            public GetListOrderItemAddedButNotPurchasedByUserIdQueryHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
            }
            public async Task<OrderItemListModel> Handle(GetListOrderItemAddedButNotPurchasedByUserIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderItem> orderItems = await _orderItemRepository.GetListAsync(index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize, include: x => x//.Include(c => c.Book)
                        .Include(x => x.User),
                    predicate: x => x.IsInTheBasket == false && x.UserId == request.UserId&&x.OrderId==null
                );

                OrderItemListModel orderItemListModel = _mapper.Map<OrderItemListModel>(orderItems);

                return orderItemListModel;




            }
        }
    }
}
