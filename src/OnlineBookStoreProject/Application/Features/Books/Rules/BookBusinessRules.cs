using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Features.Books.Rules
{
    public class BookBusinessRules
    {
        private readonly IBookRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        public BookBusinessRules(IBookRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public void BookNullCheck(Book book)
        {
            if (book == null) throw new BusinessException("Book is not exist (null)");
        }

        public async Task<Book> BookNullCheckById(int id)
        {
            Book? book = await _repository.GetAsync(x => x.Id == id);

            BookNullCheck(book);
            return book;
        }
        public async Task CategoryNullCheck(int id)
        {
            Category? category = await _categoryRepository.GetAsync(x => x.Id == id);

            if (category == null) throw new BusinessException("Category is not exist (null)");
        }




    }
}
