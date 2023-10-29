using Application.Features.Books.Dtos;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Books.Commands.CreateBook
{
    public class CreateBookCommand:IRequest<CreatedBookDto>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? CoverImagePath { get; set; }


        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreatedBookDto>
        {
            private readonly BookBusinessRules _rules;
            private readonly IBookRepository _repository;
            private readonly IMapper _mapper;

            public CreateBookCommandHandler(IBookRepository repo,IMapper mapper,BookBusinessRules businessRules)
            {
                _mapper = mapper;
                _repository = repo;
                _rules = businessRules;
            }


            public async Task<CreatedBookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
            {
                await _rules.CategoryNullCheck(request.CategoryId);

                Book mappedBook = _mapper.Map<Book>(request);
                Book createdBook = await _repository.AddAsync(mappedBook);
                CreatedBookDto createdBookDto = _mapper.Map<CreatedBookDto>(createdBook);
                return createdBookDto;
            }



        }


    }
}
