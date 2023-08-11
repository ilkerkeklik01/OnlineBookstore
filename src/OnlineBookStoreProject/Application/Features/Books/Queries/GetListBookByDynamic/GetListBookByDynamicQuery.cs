using Application.Features.Books.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Queries.GetListBookByDynamic;

public class GetListBookByDynamicQuery : IRequest<BookListModel>
{


    public PageRequest PageRequest { get; set; }
    public Dynamic Dynamic { get; set; }
    public class GetListBookByDynamicQueryHandler : IRequestHandler<GetListBookByDynamicQuery, BookListModel>
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public GetListBookByDynamicQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<BookListModel> Handle(GetListBookByDynamicQuery request, CancellationToken cancellationToken)
        {

            IPaginate<Book> books = await _repository.GetListByDynamicAsync(dynamic:request.Dynamic,
                index: request.PageRequest.Page, size: request.PageRequest.PageSize
            );

            BookListModel mappedBookListModel = _mapper.Map<BookListModel>(books);


            return mappedBookListModel;

        }


    }





}