using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Dtos;
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
            public CreateReviewCommandHandler(IReviewRepository reviewRepository,IMapper mapper, IUserRepository userRepository, IBookRepository bookRepository)
            {
                _reviewRepository = reviewRepository;
                _mapper = mapper;
                _userRepository = userRepository;
                _bookRepository = bookRepository;
            }

            public async Task<CreatedReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                Review mappedReview = _mapper.Map<Review>(request);
                mappedReview.CreatedAt =DateTime.Now;

                mappedReview.User = await _userRepository.GetAsync(u=>u.Id==mappedReview.UserId);
                mappedReview.Book = await _bookRepository.GetAsync(b=>b.Id==mappedReview.BookId);


                Review createdReview = await _reviewRepository.AddAsync(mappedReview);
                CreatedReviewDto createdReviewDto = _mapper.Map<CreatedReviewDto>(createdReview);
                return createdReviewDto;
            }




        }





    }
}
