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

namespace Application.Features.Bookshelves.Commands.CreateBookshelf
{
    public class CreateBookshelfCommand : IRequest<CreatedBookshelfDto>
    {
        public string Name { get; set; }
        public int UserId { get; set; }


        public class CreateBookshelfCommandHandler : IRequestHandler<CreateBookshelfCommand, CreatedBookshelfDto>
        {

            private readonly IBookshelfRepository _bookshelfRepository;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            public CreateBookshelfCommandHandler(IBookshelfRepository repository, IMapper mapper,IUserRepository userRepository)
            {
                _bookshelfRepository = repository;
                _mapper = mapper;
                _userRepository = userRepository;
            }

            public async Task<CreatedBookshelfDto> Handle(CreateBookshelfCommand request, CancellationToken cancellationToken)
            {
                Bookshelf mappedBookshelf = _mapper.Map<Bookshelf>(request);

                mappedBookshelf.User = await _userRepository.GetAsync(u=>u.Id==mappedBookshelf.UserId);

                Bookshelf createdBookshelf = await _bookshelfRepository.AddAsync(mappedBookshelf);
                CreatedBookshelfDto createdBookshelfDto = _mapper.Map<CreatedBookshelfDto>(createdBookshelf);
                return createdBookshelfDto;

            }



        }



    }
}
