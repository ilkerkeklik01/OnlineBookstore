using Application.Features.Books.Models;
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

namespace Application.Features.Books.Queries.GetListBook
{
    public class GetListBookQuery : IRequest<BookListModel>
    {


        public PageRequest PageRequest { get; set; }

        public class GetListBookQueryHandler : IRequestHandler<GetListBookQuery, BookListModel>
        {
            private readonly IBookRepository _repository;
            private readonly IMapper _mapper;

            public GetListBookQueryHandler(IBookRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }


            
            public async Task<BookListModel> Handle(GetListBookQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Book> books = await _repository.GetListAsync(
                    index: request.PageRequest.Page, size: request.PageRequest.PageSize
                    );

                BookListModel mappedBookListModel = _mapper.Map<BookListModel>(books);

                
                return mappedBookListModel;
            }


        }





    }
}
