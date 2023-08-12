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
            private readonly IReviewRepository _repository;
            private readonly IMapper _mapper;

            public CreateReviewCommandHandler(IReviewRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CreatedReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
            {
                Review mappedReview = _mapper.Map<Review>(request);
                mappedReview.CreatedAt =DateTime.Now;
                Review createdReview = await _repository.AddAsync(mappedReview);
                CreatedReviewDto createdReviewDto = _mapper.Map<CreatedReviewDto>(createdReview);
                return createdReviewDto;
            }




        }





    }
}
