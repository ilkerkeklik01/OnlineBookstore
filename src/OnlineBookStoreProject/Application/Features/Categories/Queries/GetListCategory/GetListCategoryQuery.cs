using Application.Features.Bookshelves.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bookshelves.Queries.GetListBookshelf;
using Application.Features.Categories.Models;

namespace Application.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery : IRequest<CategoryListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, CategoryListModel>
        {
            private readonly ICategoryRepository _repository;
            private readonly IMapper _mapper;

            public GetListCategoryQueryHandler(ICategoryRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CategoryListModel> Handle(GetListCategoryQuery request,
                CancellationToken cancellationToken)
            {
                IPaginate<Category> categories = await _repository.GetListAsync(size: request.PageRequest.PageSize,
                    index: request.PageRequest.Page);

                CategoryListModel resultModel = _mapper.Map<CategoryListModel>(categories);

                return resultModel;
            }



        }


    }
}
