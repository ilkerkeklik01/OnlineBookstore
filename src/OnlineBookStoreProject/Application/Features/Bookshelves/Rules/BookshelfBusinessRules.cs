using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Bookshelves.Rules
{
    public class BookshelfBusinessRules
    {

        private readonly IUserRepository _userRepository;

        public BookshelfBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserNullCheck(int userId)
        {
            User? user= await _userRepository.GetAsync(x => x.Id == userId);


            if (user==null)
            {
                throw new BusinessException("User is not exist!");
            }

        }


    }
}
