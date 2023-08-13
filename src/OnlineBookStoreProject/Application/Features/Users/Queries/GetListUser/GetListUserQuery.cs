using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.GetListUser
{
    public class GetListUserQuery : IRequest<UserListModel>
    {
        public PageRequest PageRequest { get; set; }


        public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, UserListModel>
        {

            private readonly IUserRepository _repository;
            private readonly IMapper _mapper;

            public GetListUserQueryHandler(IUserRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<UserListModel> Handle(GetListUserQuery request, CancellationToken cancellationToken)
            {
                IPaginate<User> users = await _repository.GetListAsync(
                    size: request.PageRequest.PageSize,index:request.PageRequest.Page
                    );

                UserListModel userListModel = _mapper.Map<UserListModel>(users);
                return userListModel;

            }



        }


    }



}
