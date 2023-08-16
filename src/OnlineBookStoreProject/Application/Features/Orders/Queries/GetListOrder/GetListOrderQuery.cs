using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetListOrder
{
    public class GetListOrderQuery : IRequest<OrderListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListOrderQueryHandler : IRequestHandler<GetListOrderQuery,OrderListModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderRepository _repository;

            public GetListOrderQueryHandler(IMapper mapper, IOrderRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            public async Task<OrderListModel> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Order> orders = await _repository.GetListAsync(
                    include:o=>o.Include(order=>order.User).Include(x=>x.OrderItems).ThenInclude(x=>x.Book),
                    size:request.PageRequest.PageSize,index:request.PageRequest.Page
                    );

                OrderListModel orderListModel = _mapper.Map<OrderListModel>( orders );
                return orderListModel;
            }


        }



    }
}
