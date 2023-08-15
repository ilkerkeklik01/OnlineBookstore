using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.OrderItems.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateOrderItemCommand,OrderItem>();
            CreateMap<OrderItem,CreatedOrderItemDto>().ForMember(c=>c.BookTitle,
                opt=>opt.MapFrom(c=>c.Book.Title)).
                ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username)
            ).ForMember(x => x.Discount,
                    opt => opt.MapFrom(x => x.Book.Discount)
                ); 

            CreateMap<IPaginate<OrderItem>,OrderItemListModel>();
            CreateMap<OrderItem, OrderItemListDto>().ForMember(c => c.BookTitle,
                opt => opt.MapFrom(c => c.Book.Title)).ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username)
            ).ForMember(x => x.Discount,
                opt => opt.MapFrom(x => x.Book.Discount)
            );

            CreateMap<OrderItem,OrderItemDto>().ForMember(c => c.BookTitle,
                opt => opt.MapFrom(c => c.Book.Title)).ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username)
            ).ForMember(x => x.Discount,
                opt => opt.MapFrom(x => x.Book.Discount)
            );
            ;





        }
    }
}
