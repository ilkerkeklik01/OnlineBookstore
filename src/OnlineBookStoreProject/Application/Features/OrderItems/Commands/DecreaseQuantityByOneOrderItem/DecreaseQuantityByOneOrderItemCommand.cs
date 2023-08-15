using Application.Features.OrderItems.Commands.IncreaseQuantityByOneOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Rules;

namespace Application.Features.OrderItems.Commands.DecreaseQuantityByOneOrderItem
{
    public class DecreaseQuantityByOneOrderItemCommand : IRequest<OrderItemDto>
    {

        public int Id { get; set; }


        public class DecreaseQuantityByOneOrderItemCommandHandler : IRequestHandler<DecreaseQuantityByOneOrderItemCommand, OrderItemDto>
        {
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMapper _mapper;
            private readonly IBookRepository _bookRepository;
            private readonly IUserRepository _userRepository;
            private readonly OrderItemBusinessRules _businessRules;
            public DecreaseQuantityByOneOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper,
                IBookRepository bookRepository, IUserRepository userRepository,OrderItemBusinessRules businessRules)
            {
                _orderItemRepository = orderItemRepository;
                _mapper = mapper;
                _bookRepository = bookRepository;
                _userRepository = userRepository;
                _businessRules=businessRules;
            }

            //Olmayan bir id yi referans olarak veremeyecek şekilde bir iş kuralı yazacagım
            //Quantitysi 0 olursa olmaz 1 olursa da 0 yapıp is in the basket i false yapmalıyım.
            public async Task<OrderItemDto> Handle(DecreaseQuantityByOneOrderItemCommand request, CancellationToken cancellationToken)
            {
                OrderItem orderItem = await _businessRules.QuantityOfOrderItemCannotBeLessThanZeroAndNullCheckFirst(request.Id);


                bool basketCheck;

                orderItem.Quantity--;

                basketCheck= orderItem.Quantity == 0;
                if (basketCheck)
                {
                    orderItem.IsInTheBasket = false;
                }

                //While creating order items I Already initialize the books and users. but maybe they changed..
                orderItem.Book = await _bookRepository.GetAsync(x => x.Id == orderItem.BookId);
                orderItem.User = await _userRepository.GetAsync(x => x.Id == orderItem.UserId);

                OrderItem updatedOrderItem = await _orderItemRepository.UpdateAsync(orderItem);
                OrderItemDto orderItemDto = _mapper.Map<OrderItemDto>(updatedOrderItem);

                return orderItemDto;
            }
            
        }






    }
}
