using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.OrderItems.Commands.IncreaseQuantityByOneOrderItem
{
    public class IncreaseQuantityByOneOrderItemCommand : IRequest<OrderItemDto>
    {
        public int Id { get; set; }


        public class IncreaseQuantityByOneOrderItemCommandHandler : IRequestHandler<IncreaseQuantityByOneOrderItemCommand,OrderItemDto>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;
            private readonly IBookRepository _bookRepository;
            private readonly IUserRepository _userRepository;
            private readonly OrderItemBusinessRules _businessRules;
            public IncreaseQuantityByOneOrderItemCommandHandler(IOrderItemRepository orderItemRepository,
                IMapper mapper, IBookRepository bookRepository, IUserRepository userRepository,OrderItemBusinessRules businessRules)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
                _bookRepository = bookRepository;
                _userRepository = userRepository;
                _businessRules = businessRules;
            }

            //Olmayan bir id yi referans olarak veremeyecek şekilde bir iş kuralı yazacagım
            //10 dan fazla ürün alamaması için de bir iş kuralı yazacagım
            public async Task<OrderItemDto> Handle(IncreaseQuantityByOneOrderItemCommand request, CancellationToken cancellationToken)
            {
                var asd = await _orderItemRepository.GetAsync(x => x.Id == request.Id);


                OrderItem orderItem = await _businessRules.QuantityOfOrderItemCannotBeMoreThanTenAndNullCheckFirst(request.Id);
                await _businessRules.CannotIncreseTheQuantityOfOrderItemNotInTheBasket(orderItem.IsInTheBasket);

                orderItem.Quantity++;

                //While creating order items I Already initialize the books and users. but maybe they changed..
                orderItem.Book = await _bookRepository.GetAsync(x => x.Id == orderItem.BookId);
                orderItem.User = await _userRepository.GetAsync(x => x.Id == orderItem.UserId);

                //orderItem = _mapper.Map<OrderItem>(orderItem.Book);//*****
                _mapper.Map(orderItem.Book, orderItem);//*******


                OrderItem updatedOrderItem = await _orderItemRepository.UpdateAsync(orderItem);
                OrderItemDto orderItemDto = _mapper.Map<OrderItemDto>(updatedOrderItem);
                
                return orderItemDto;
            }


        }


    }
}
