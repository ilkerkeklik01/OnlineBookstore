using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Features.Reviews.Rules
{
    public class ReviewBusinessRules
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        public ReviewBusinessRules(IUserRepository userRepository, IReviewRepository reviewRepository, IBookRepository bookRepository)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
        }

        public async Task<User> UserNullCheck(int userId)
        {
            User? user = await _userRepository.GetAsync(x => x.Id == userId);
            bool userCheck = user != null;

            if (!userCheck)
            {
                throw new BusinessException("User is not exist!");
            }
            return user;
        }

        public void BookNullCheck(Book book)
        {
            if (book == null) throw new BusinessException("Book is not exist (null)");
        }

        public async Task<Book> BookNullCheckById(int id)
        {
            Book? book = await _bookRepository.GetAsync(x => x.Id == id);

            BookNullCheck(book);
            return book;
        }

    }
}
