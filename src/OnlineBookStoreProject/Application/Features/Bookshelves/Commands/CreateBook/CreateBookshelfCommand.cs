using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bookshelves.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Bookshelves.Commands.CreateBook
{
    public class CreateBookshelfCommand : IRequest<CreatedBookshelfDto>
    {
        public string Name { get; set; }
        public int UserId { get; set; }


        public class CreateBookshelfCommandHandler : IRequestHandler<CreateBookshelfCommand, CreatedBookshelfDto>
        {

            private readonly IBookshelfRepository _repository;
            private readonly IMapper _mapper;

            public CreateBookshelfCommandHandler(IBookshelfRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CreatedBookshelfDto> Handle(CreateBookshelfCommand request, CancellationToken cancellationToken)
            {
                Bookshelf mappedBookshelf = _mapper.Map<Bookshelf>(request);
                Bookshelf createdBookshelf = await _repository.AddAsync(mappedBookshelf);
                CreatedBookshelfDto createdBookshelfDto = _mapper.Map<CreatedBookshelfDto>(createdBookshelf);
                return createdBookshelfDto;
            }



        }



    }
}
