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

        public BookBusinessRules(IBookRepository repository)
        {
            _repository = repository;
        }

        public void BookNullCheck(Book book)
        {
            if (book == null) throw new BusinessException("Book is not exist (null)");
        }
       


        
    }
}
