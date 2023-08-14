using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; } = 1;
        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemDto>
        {
            private readonly IOrderItemRepository _orderRepository;
            private readonly IMapper _mapper;
            private readonly IBookRepository _bookRepository;

            public CreateOrderItemCommandHandler(IOrderItemRepository repository,IMapper mapper,IBookRepository bookRepository)
            {
                _orderRepository = repository;
                _mapper = mapper;
            }


            public async Task<CreatedOrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                OrderItem mappedOrderItem = _mapper.Map<OrderItem>(request);
                mappedOrderItem.IsInTheBasket = true;
                
                mappedOrderItem.Book = await _bookRepository.GetAsync(b => b.Id == mappedOrderItem.BookId);

                OrderItem createdOrderItem = await _orderRepository.AddAsync(mappedOrderItem);
                CreatedOrderItemDto createdOrderItemDto = _mapper.Map<CreatedOrderItemDto>(createdOrderItem);
                return createdOrderItemDto;



            }


        }


    }
}
