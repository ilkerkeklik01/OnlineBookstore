using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.BasketItems.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BasketItems.Queries.GetListBasketItem
{
    public class GetListBasketItemQuery : IRequest<BasketItemListModel>
    {

        public PageRequest PageRequest { get; set; }


        public class GetListBasketItemQueryHandler : IRequestHandler<GetListBasketItemQuery, BasketItemListModel>
        {

            private readonly IMapper _mapper;
            private readonly IBasketItemRepository _basketItemRepository;


            public GetListBasketItemQueryHandler(IMapper mapper, IBasketItemRepository basketItemRepository)
            {
                _mapper = mapper;
                _basketItemRepository = basketItemRepository;
            }

            public async Task<BasketItemListModel> Handle(GetListBasketItemQuery request, CancellationToken cancellationToken)
            {
                IPaginate<BasketItem> basketItems = await _basketItemRepository.GetListAsync(include:m
                    =>m.Include(x=>x.User),size:request.PageRequest.PageSize,index:request.PageRequest.Page);

                BasketItemListModel model = _mapper.Map<BasketItemListModel>(basketItems);
                return model;
            }


        }


    }
}
