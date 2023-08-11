using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Categories.Profiles
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<CreateCategoryCommand,Category>();
            CreateMap<Category,CreatedCategoryDto>();
            CreateMap<IPaginate<Category>,CategoryListModel>();
            CreateMap<Category,CategoryListDto>();
        }

    }
}
