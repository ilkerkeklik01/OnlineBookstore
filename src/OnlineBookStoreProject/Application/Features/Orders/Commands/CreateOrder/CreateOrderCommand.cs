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
            private readonly IOrderRepository _repository;
            private readonly IMapper _mapper;

            public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }


            public async Task<CreatedOrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {   
                Order mappedOrder = _mapper.Map<Order>(request);
                mappedOrder.OrderDate=DateTime.Now;
                Order createdOrder = await _repository.AddAsync(mappedOrder);
                CreatedOrderDto createdOrderDto = _mapper.Map<CreatedOrderDto>(createdOrder);
                return createdOrderDto;

            }



        }




    }
}
