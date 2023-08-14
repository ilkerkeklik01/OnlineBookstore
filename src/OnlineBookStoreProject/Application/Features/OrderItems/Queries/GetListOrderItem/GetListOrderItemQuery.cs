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

namespace Application.Features.OrderItems.Queries.GetListOrderItem
{
    public class GetListOrderItemQuery : IRequest<OrderItemListModel>
    {

        public PageRequest PageRequest { get; set; }

        public class GetListOrderItemQueryHandler : IRequestHandler<GetListOrderItemQuery, OrderItemListModel>
        {

            private readonly IOrderItemRepository _repository;
            private readonly IMapper _mapper;

            public GetListOrderItemQueryHandler(IOrderItemRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<OrderItemListModel> Handle(GetListOrderItemQuery request, CancellationToken cancellationToken)
            {
                IPaginate<OrderItem> orderItems = await _repository.GetListAsync(index:request.PageRequest.Page,
                    size:request.PageRequest.PageSize,include:x=>x.Include(c=>c.Book));
                OrderItemListModel orderItemListModel = _mapper.Map<OrderItemListModel>(orderItems);

                return orderItemListModel;
            }


        }


    }
}
