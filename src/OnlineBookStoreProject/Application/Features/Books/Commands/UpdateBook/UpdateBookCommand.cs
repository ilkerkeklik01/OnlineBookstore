﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<UpdatedBookDto>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime PublicationDate { get; set; }
        public string CoverImagePath { get; set; }


        public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdatedBookDto>
        {
            private readonly BookBusinessRules _rules;
            private readonly IBookRepository _repository;
            private readonly IMapper _mapper;
            private readonly IOrderItemRepository _orderItemRepository;

            public UpdateBookCommandHandler(IBookRepository repo, IMapper mapper, BookBusinessRules businessRules, IOrderItemRepository orderItemRepository)
            {
                _mapper = mapper;
                _repository = repo;
                _rules = businessRules;
                _orderItemRepository = orderItemRepository;
            }


            public async Task<UpdatedBookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
            {
                Book oldBook = await _rules.BookNullCheckById(request.Id);

                _mapper.Map(request,oldBook);

                



                Book updatedBook = await _repository.UpdateAsync(oldBook);

                //Updating order items which are in the basket of any user
                await UpdateOrderItemsWhichAreInTheBasketOfAnyUser(updatedBook);

                
                UpdatedBookDto updatedBookDto = _mapper.Map<UpdatedBookDto>(updatedBook);
                return updatedBookDto;
            }


            private async Task UpdateOrderItemsWhichAreInTheBasketOfAnyUser(Book updatedBook)
            {
                IPaginate<OrderItem> paginate =  await _orderItemRepository.GetListAsync(x=>x.IsInTheBasket==true&&x.BookId==updatedBook.Id);
                
                List<OrderItem> orderItems = paginate.Items.ToList<OrderItem>();

                //These order items will be updated
                foreach (OrderItem item in orderItems)
                {
                    _mapper.Map(updatedBook,item);
                    await _orderItemRepository.UpdateAsync(item);
                }

            }

        }


    }
}
