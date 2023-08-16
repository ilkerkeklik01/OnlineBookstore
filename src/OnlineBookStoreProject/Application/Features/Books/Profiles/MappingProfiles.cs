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
    public class MappingProfiles: Profile
    {

        public MappingProfiles() {
            CreateMap<CreateBookCommand,Book>();//ReverseMap
            CreateMap<Book, CreatedBookDto>();
            CreateMap<IPaginate<Book>, BookListModel>();
            CreateMap<Book, BookListDto>();
            CreateMap<Book, BookDto>();

            CreateMap<UpdateBookCommand, Book>();
            CreateMap<Book, UpdatedBookDto>();

        }

    }
}
