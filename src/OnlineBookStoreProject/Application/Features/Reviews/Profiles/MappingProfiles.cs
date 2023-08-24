using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Reviews.Profiles
{
    public class MappingProfiles : Profile, IMappingProfile
    {

        public MappingProfiles()
        {
            CreateMap<CreateReviewCommand,Review>();
            CreateMap<Review,CreatedReviewDto>().ForMember(c=>c.UserName,
                opt=>opt.MapFrom(c=>c.User.Username))
                .ForMember(c=>c.BookTitle,
                    opt=>opt.MapFrom(c=>c.Book.Title));

            CreateMap<IPaginate<Review>,ReviewListModel>();
            CreateMap<Review,ReviewListDto>().ForMember(c=>c.BookTitle,
                opt=>opt.MapFrom(c=>c.Book.Title))
                .ForMember(c=>c.UserName,
                    opt=>opt.MapFrom(e=>e.User.Username));

        }
    }
}
