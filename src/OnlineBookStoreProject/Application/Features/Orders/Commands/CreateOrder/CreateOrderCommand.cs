using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand: IRequest<CreatedOrderDto>
    {
        public int UserId { get; set; }
        
        //public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreatedOrderDto>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper, IUserRepository userRepository)
            {
                _orderRepository = repository;
                _mapper = mapper;
                _userRepository = userRepository;
            }


            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {   
                Order mappedOrder = _mapper.Map<Order>(request);
                mappedOrder.OrderDate=DateTime.Now;

                mappedOrder.User = await _userRepository.GetAsync(b => b.Id == mappedOrder.UserId);


                Order createdOrder = await _orderRepository.AddAsync(mappedOrder);
                CreatedOrderDto createdOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);
                return createdOrderDto;

            }



        }




    }
}
