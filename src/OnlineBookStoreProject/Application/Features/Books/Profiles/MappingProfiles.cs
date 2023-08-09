using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Profiles
{
    public class MappingProfiles: Profile
    {

        public MappingProfiles() {
            CreateMap<CreateBookCommand,Book>();//ReverseMap
            CreateMap<Book, CreatedBookDto>();
            
        }
    }
}
