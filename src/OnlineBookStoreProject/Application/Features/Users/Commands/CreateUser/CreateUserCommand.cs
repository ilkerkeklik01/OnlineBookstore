using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
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
            private readonly UserBusinessRules _userBusinessRules;
            public CreateUserCommandHandler(IUserRepository repository,IMapper mapper, UserBusinessRules userBusinessRules)
            {
                _repository = repository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
            }


            public async Task<CreatedUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.EmailCannotBeDuplicated(request.Email);

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
