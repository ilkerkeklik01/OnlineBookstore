using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.GetListReview
{
    public class GetListReviewQuery : IRequest<ReviewListModel>
    {

        public PageRequest PageRequest { get; set; }


        public class GetListReviewQueryHandler : IRequestHandler<GetListReviewQuery, ReviewListModel>
        {
            private readonly IReviewRepository _repository;
            private readonly IMapper _mapper;


            public GetListReviewQueryHandler(IReviewRepository repository,IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }


            public async Task<ReviewListModel> Handle(GetListReviewQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Review> reviews = await _repository.GetListAsync(
                    index:request.PageRequest.Page,
                    size:request.PageRequest.PageSize,
                    include:m=>m.Include(e=>e.User)
                        .Include(e=>e.Book)
                    );
                ReviewListModel reviewListModel = _mapper.Map<ReviewListModel>(reviews);
                return reviewListModel;
            }



        }


    }
}
