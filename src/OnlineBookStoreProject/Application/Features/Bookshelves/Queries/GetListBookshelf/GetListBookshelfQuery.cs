using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bookshelves.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookshelves.Queries.GetListBookshelf
{
    public class GetListBookshelfQuery : IRequest<BookshelfListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListBookshelfQueryHandler : IRequestHandler<GetListBookshelfQuery, BookshelfListModel>
        {
            private readonly IBookshelfRepository _repository;
            private readonly IMapper _mapper;

            public GetListBookshelfQueryHandler(IBookshelfRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<BookshelfListModel> Handle(GetListBookshelfQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Bookshelf> bookshelves = await _repository.GetListAsync(size:request.PageRequest.PageSize,
                    index:request.PageRequest.Page,include:m=>m.Include(c=>c.User));

                BookshelfListModel resultModel = _mapper.Map<BookshelfListModel>(bookshelves);
                
                return resultModel;
            }



        }


    }
}
