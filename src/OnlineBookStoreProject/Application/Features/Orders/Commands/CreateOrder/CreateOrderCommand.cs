using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand: IRequest<CreatedOrderDto>
    {
        public int UserId { get; set; }
        
        //public DateTime OrderDate { get; set; }
        //public decimal TotalPrice { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly OrderBusinessRules _orderBusinessRules;
            private readonly IBookRepository _bookRepository;
            public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper, IUserRepository userRepository, IOrderItemRepository orderItemRepository, OrderBusinessRules orderBusinessRules, IBookRepository bookRepository)
            {
                _orderRepository = repository;
                _mapper = mapper;
                _userRepository = userRepository;
                _orderItemRepository = orderItemRepository;
                _orderBusinessRules = orderBusinessRules;
                _bookRepository = bookRepository;
            }


            //burda order üretilmesi için userın sepetinde bir şey olması lazım kuralını yazmalıyım
            //order  nesnesinin 

            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                await _orderBusinessRules.UserNullCheckAndCheckTheBasketOfUserIsEmpty(request.UserId);
                
                Order mappedOrder = _mapper.Map<Order>(request);
                mappedOrder.OrderDate=DateTime.Now;

                mappedOrder.User = await _userRepository.GetAsync(b => b.Id == mappedOrder.UserId);
                IPaginate<OrderItem> orderItems = await _orderItemRepository.GetListAsync(x=>x.IsInTheBasket==true&&
                    x.UserId==request.UserId
                    );


                mappedOrder.OrderItems = orderItems.Items;

                decimal totalPrice = 0;



                Order createdOrder = await _orderRepository.AddAsync(mappedOrder);

                createdOrder.TotalPrice = await GetTotalPriceOfOrder(createdOrder.OrderItems);
                await RemoveOrderedOrderItemsFromTheBasketOfUserAndFillOrderId(createdOrder.OrderItems,createdOrder.Id);
                
                
                CreatedOrderDto createdOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);
                return createdOrderDto;

            }

            private async Task RemoveOrderedOrderItemsFromTheBasketOfUserAndFillOrderId(ICollection<OrderItem> orderItems,int orderId)
            {
                foreach (OrderItem each in orderItems)
                {
                    each.IsInTheBasket = false;
                    each.OrderId = orderId;
                    await _orderItemRepository.UpdateAsync(each);
                }

            }

            private async Task<decimal> GetTotalPriceOfOrder(ICollection<OrderItem> orderItems)
            {
                decimal totalPrice = 0;

                foreach (OrderItem each in orderItems)
                {   
                    //try
                    each.Book = await _bookRepository.GetAsync(x=>x.Id==each.BookId);
                    decimal discount = each.Book.Discount;
                    decimal price = each.Book.Price;
                    decimal net = price - discount;
                    totalPrice += net * each.Quantity;
                }

                return totalPrice;
            }



        }




    }
}
