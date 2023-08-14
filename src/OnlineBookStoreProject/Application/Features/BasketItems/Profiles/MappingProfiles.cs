using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.BasketItems.Commands.CreateBasketItem;
using Application.Features.BasketItems.Dtos;
using Application.Features.BasketItems.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.BasketItems.Profiles
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateBasketItemCommand, BasketItem>();
            CreateMap<BasketItem, CreatedBasketItemDto>().ForMember(x=>x.UserName,
                opt=>opt.MapFrom(x=>x.User.Username));

            CreateMap<IPaginate<BasketItem>,BasketItemListModel>();
            CreateMap<BasketItem,BasketItemListDto>().ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username));
            
        }

    }
}
