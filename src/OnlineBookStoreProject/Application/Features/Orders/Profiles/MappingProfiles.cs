using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Orders.Profiles
{
    public class MappingProfiles :Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateOrderCommand,Order>();
            CreateMap<Order,CreatedOrderDto>().ForMember(c=>c.UserName,
                opt=>opt.MapFrom(c=>c.User.Username)
                );
            CreateMap<IPaginate<Order>,OrderListModel>();
            CreateMap<Order,OrderListDto>().ForMember(c=>c.UserName,
                opt=>opt.MapFrom(c=>c.User.Username));

        }
    }
}
