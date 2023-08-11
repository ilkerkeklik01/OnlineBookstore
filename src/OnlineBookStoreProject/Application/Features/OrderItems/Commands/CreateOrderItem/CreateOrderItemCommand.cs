using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Routing.Tree;

namespace Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest<CreatedOrderItemDto>
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemDto>
        {
            private readonly IOrderItemRepository _repository;
            private readonly IMapper _mapper;

            public CreateOrderItemCommandHandler(IOrderItemRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }


            public async Task<CreatedOrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                OrderItem mappedOrderItem = _mapper.Map<OrderItem>(request);
                OrderItem createdOrderItem = await _repository.AddAsync(mappedOrderItem);
                CreatedOrderItemDto createdOrderItemDto = _mapper.Map<CreatedOrderItemDto>(createdOrderItem);
                return createdOrderItemDto;
            }


        }


    }
}
