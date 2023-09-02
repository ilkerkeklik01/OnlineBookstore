using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<CreatedReviewDto>
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public string ReviewText { get; set; }
        //public DateTime CreatedAt { get; set; }



        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreatedReviewDto>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly IBookRepository _bookRepository;
            private readonly ReviewBusinessRules _reviewBusinessRules;
            public CreateReviewCommandHandler(IReviewRepository reviewRepository,IMapper mapper, IUserRepository userRepository, IBookRepository bookRepository, ReviewBusinessRules reviewBusinessRules)
            {
                _reviewRepository = reviewRepository;
                _mapper = mapper;
                _userRepository = userRepository;
                _bookRepository = bookRepository;
                _reviewBusinessRules = reviewBusinessRules;
            }

            public async Task<CreatedReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                User? user = await _reviewBusinessRules.UserNullCheck(request.UserId);
                Book? book = await _reviewBusinessRules.BookNullCheckById(request.BookId);

                Review mappedReview = _mapper.Map<Review>(request);
                mappedReview.CreatedAt =DateTime.Now;

                mappedReview.User = user;
                mappedReview.Book = book;


                Review createdReview = await _reviewRepository.AddAsync(mappedReview);
                CreatedReviewDto createdReviewDto = _mapper.Map<CreatedReviewDto>(createdReview);
                return createdReviewDto;
            }




        }





    }
}
