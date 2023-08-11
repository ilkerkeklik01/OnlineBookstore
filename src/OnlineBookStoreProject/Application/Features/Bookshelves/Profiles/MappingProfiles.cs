using Application.Features.Bookshelves.Dtos;
using Application.Features.Bookshelves.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Bookshelves.Profiles;

public class MappingProfiles:Profile
{

    public MappingProfiles()
    {
        CreateMap<Bookshelf,BookshelfListDto>().ForMember(c=>c.UserName,
            opt=>opt.MapFrom(c=>c.User.Username));
        CreateMap<IPaginate<Bookshelf>,BookshelfListModel>();
    }
}