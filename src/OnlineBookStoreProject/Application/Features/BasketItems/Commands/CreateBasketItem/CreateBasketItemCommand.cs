using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.BasketItems.Dtos;
using Application.Features.Books.Dtos;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.BasketItems.Commands.CreateBasketItem
{
    public class CreateBasketItemCommand : IRequest<CreatedBasketItemDto>
    {

        public int UserId { get; set; }
        public int OrderItemId { get; set; }

        public class CreateBasketItemCommandHandler : IRequestHandler<CreateBasketItemCommand, CreatedBasketItemDto>
        {

            private readonly IBasketItemRepository _repository;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public CreateBasketItemCommandHandler(IBasketItemRepository repo, IMapper mapper, IUserRepository userRepository)
            {
                _mapper = mapper;
                _repository = repo;
                _userRepository = userRepository;
            }

            public async Task<CreatedBasketItemDto> Handle(CreateBasketItemCommand request, CancellationToken cancellationToken)
            {
                BasketItem mappedBasketItem = _mapper.Map<BasketItem>(request);
                
                mappedBasketItem.User = await _userRepository.GetAsync(x => x.Id == request.UserId);

                BasketItem createdBasketItem= await _repository.AddAsync(mappedBasketItem);
                CreatedBasketItemDto createdBasketItemDto= _mapper.Map<CreatedBasketItemDto>(createdBasketItem);
                return createdBasketItemDto;
            }

        }



    }
}
