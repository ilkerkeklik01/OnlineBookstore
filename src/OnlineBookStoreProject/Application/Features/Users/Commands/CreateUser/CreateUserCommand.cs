using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreatedUserDto>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public DateTime RegistrationDate { get; set; }
        //public DateTime? PasswordUpdatedAt { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserDto>
        {
            private readonly IUserRepository _repository;
            private readonly IMapper _mapper;
            public CreateUserCommandHandler(IUserRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }


            public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User mappedUser = _mapper.Map<User>(request);
                mappedUser.PasswordUpdatedAt=DateTime.Now;
                mappedUser.RegistrationDate=DateTime.Now;

                User createdUser = await _repository.AddAsync(mappedUser);

                CreatedUserDto createdUserDto = _mapper.Map<CreatedUserDto>(createdUser);
                return createdUserDto;
            }




        }





    }
}
