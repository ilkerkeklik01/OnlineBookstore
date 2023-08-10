using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Dtos;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Queries.GetByIdBook
{
    public class GetByIdBookQuery:IRequest<BookDto>
    {

        public int Id { get; set; }


        public class GetByIdBookQueryHandler : IRequestHandler<GetByIdBookQuery, BookDto>
        {
            private readonly IBookRepository _repository;
            private readonly IMapper _mapper;
            private readonly BookBusinessRules _rules;
            public GetByIdBookQueryHandler(IBookRepository repository,IMapper mapper , BookBusinessRules rules)
            {
                _repository = repository;
                _mapper = mapper;
                _rules = rules;
            }
            public async Task<BookDto> Handle(GetByIdBookQuery request, CancellationToken cancellationToken)
            {
                Book? book = await _repository.GetAsync(x=>x.Id==request.Id);
                
                _rules.BookNullCheck(book);

                BookDto result = _mapper.Map<BookDto>(book);
                return result;

            }



        }

    }
}
