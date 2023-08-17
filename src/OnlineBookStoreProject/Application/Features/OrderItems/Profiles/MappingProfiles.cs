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
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateOrderItemCommand, OrderItem>();


            CreateMap<Book, OrderItem>()
                .ForMember(x => x.Id, opt => opt.Ignore())//WARNING ID MAPPING IGNORED
                .ForMember(x => x.BookTitleAtThatTime, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.BookAuthorAtThatTime, opt => opt.MapFrom(x => x.Author))
                .ForMember(x => x.BookCategoryIdAtThatTime, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.BookDescriptionAtThatTime, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.BookPriceAtThatTime, opt => opt.MapFrom(x => x.Price))
                .ForMember(x => x.BookDiscountAtThatTime, opt => opt.MapFrom(x => x.Discount))
                .ForMember(x => x.BookPublicationDateAtThatTime, opt => opt.MapFrom(x => x.PublicationDate))
                .ForMember(x => x.BookCoverImagePathAtThatTime, opt => opt.MapFrom(x => x.CoverImagePath));


            CreateMap<OrderItem, CreatedOrderItemDto>().ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username));

            //CreateMap<OrderItem,CreatedOrderItemDto>().ForMember(c=>c.BookTitle,
            //    opt=>opt.MapFrom(c=>c.Book.Title)).
            //    ForMember(x => x.UserName,
            //    opt => opt.MapFrom(x => x.User.Username)
            //).ForMember(x => x.Discount,
            //        opt => opt.MapFrom(x => x.Book.Discount)
            //    ).ForMember(x => x.BookPrice,
            //        opt => opt.MapFrom(x => x.Book.Price)
            //    ); 



            CreateMap<IPaginate<OrderItem>, OrderItemListModel>();
            CreateMap<OrderItem, OrderItemListDto>().ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username));
            //CreateMap<OrderItem, OrderItemListDto>().ForMember(c => c.BookTitle,
            //    opt => opt.MapFrom(c => c.Book.Title)).ForMember(x => x.UserName,
            //    opt => opt.MapFrom(x => x.User.Username)
            //).ForMember(x => x.Discount,
            //    opt => opt.MapFrom(x => x.Book.Discount)
            //).ForMember(x => x.BookPrice,
            //    opt => opt.MapFrom(x => x.Book.Price)
            //);
            CreateMap<OrderItem, OrderItemDto>().ForMember(x => x.UserName,
                opt => opt.MapFrom(x => x.User.Username));
            //CreateMap<OrderItem,OrderItemDto>().ForMember(c => c.BookTitle,
            //    opt => opt.MapFrom(c => c.Book.Title)).ForMember(x => x.UserName,
            //    opt => opt.MapFrom(x => x.User.Username)
            //).ForMember(x => x.Discount,
            //    opt => opt.MapFrom(x => x.Book.Discount)
            //).ForMember(x => x.BookPrice,
            //    opt => opt.MapFrom(x => x.Book.Price)
            //);






        }
    }
}
