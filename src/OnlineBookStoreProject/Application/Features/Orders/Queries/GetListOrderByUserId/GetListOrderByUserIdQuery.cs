﻿using Application.Features.Orders.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetListOrderByUserId
{
    public class GetListOrderByUserIdQuery : IRequest<OrderListModel>
    {
        public int UserId { get; set; }
        public PageRequest PageRequest { get; set; }


        public class GetListOrderByUserIdQueryHandler : IRequestHandler<GetListOrderByUserIdQuery, OrderListModel>
        {
            private readonly IMapper _mapper;
            private readonly IOrderRepository _repository;

            public GetListOrderByUserIdQueryHandler(IMapper mapper, IOrderRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }



            public async Task<OrderListModel> Handle(GetListOrderByUserIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Order> orders = await _repository.GetListAsync(
                    include: o => o.Include(order => order.User).Include(x => x.OrderItems),  //Thenİnclude book
                    size: request.PageRequest.PageSize, index: request.PageRequest.Page,
                    predicate: x => x.UserId == request.UserId
                );

                OrderListModel orderListModel = _mapper.Map<OrderListModel>(orders);
                return orderListModel;
            }




        }


    }
}
