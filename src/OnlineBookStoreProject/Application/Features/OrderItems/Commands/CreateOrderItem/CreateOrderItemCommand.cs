using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Routing.Tree;

namespace Application.Features.OrderItems.Commands.CreateOrderItem
{
    public class CreateOrderItemCommand : IRequest<CreatedOrderItemDto>
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        //public int? Quantity { get; set; } = 1;
        public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CreatedOrderItemDto>
        {
            private readonly IOrderItemRepository _orderRepository;
            private readonly IMapper _mapper;
            private readonly IBookRepository _bookRepository;
            private readonly IUserRepository _userRepository;
            private readonly OrderItemBusinessRules _businessRules;
            public CreateOrderItemCommandHandler(IOrderItemRepository repository,IMapper mapper,IBookRepository bookRepository, IUserRepository userRepository, OrderItemBusinessRules businessRules)
            {
                _orderRepository = repository;
                _mapper = mapper;
                _bookRepository = bookRepository;
                _userRepository = userRepository;
                _businessRules = businessRules;
            }


            public async Task<CreatedOrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.CannotCreateOrderItemWithExistingBookIfItIsInTheBasketOfTheSameUser(request.BookId,
                    request.UserId);

                OrderItem mappedOrderItem = _mapper.Map<OrderItem>(request);
                
                mappedOrderItem.IsInTheBasket = true;
                mappedOrderItem.Quantity = 1;
                mappedOrderItem.Book = await _bookRepository.GetAsync(b => b.Id == mappedOrderItem.BookId);
                mappedOrderItem.User = await _userRepository.GetAsync(u => u.Id == mappedOrderItem.UserId);

                //mappedOrderItem = _mapper.Map<OrderItem>(mappedOrderItem.Book);//*****
                _mapper.Map(mappedOrderItem.Book, mappedOrderItem);//******* WARNING


                OrderItem createdOrderItem = await _orderRepository.AddAsync(mappedOrderItem);
                CreatedOrderItemDto createdOrderItemDto = _mapper.Map<CreatedOrderItemDto>(mappedOrderItem);
                createdOrderItemDto.Id = createdOrderItem.Id;
                return createdOrderItemDto;

            }


        }


    }
}
