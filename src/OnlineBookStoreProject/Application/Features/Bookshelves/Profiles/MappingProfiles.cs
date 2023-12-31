﻿using Application.Features.Bookshelves.Commands.CreateBookshelf;
using Application.Features.Bookshelves.Dtos;
using Application.Features.Bookshelves.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Bookshelves.Profiles;

public class MappingProfiles:Profile,IMappingProfile
{

    public MappingProfiles()
    {


        CreateMap<Bookshelf, BookshelfListDto>().ForMember(c => c.UserName,
            opt => opt.MapFrom(c => c.User!=null ? c.User.Username : "HATAAA"));

        CreateMap<IPaginate<Bookshelf>,BookshelfListModel>();
        


        CreateMap<CreateBookshelfCommand,Bookshelf>();

        CreateMap<Bookshelf, CreatedBookshelfDto>().ForMember(c=>c.UserName,
            opt=>opt.MapFrom(c=>c.User.Username));
    }
}