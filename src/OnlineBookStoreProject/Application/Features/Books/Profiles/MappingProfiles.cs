using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.UpdateBook;

namespace Application.Features.Books.Profiles
{
    public class MappingProfiles: Profile,IMappingProfile
    {

        public MappingProfiles() {
            CreateMap<CreateBookCommand,Book>();//ReverseMap
            CreateMap<Book, CreatedBookDto>();
            CreateMap<IPaginate<Book>, BookListModel>();
            CreateMap<Book, BookListDto>();
            CreateMap<Book, BookDto>();

            CreateMap<UpdateBookCommand, Book>();
            CreateMap<Book, UpdatedBookDto>();

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


        }

    }
}
