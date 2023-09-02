using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Models;
using Application.Features.Reviews.Queries.GetListReview;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Review
{
    public class GetListReviewQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly GetListReviewQuery.GetListReviewQueryHandler _sut;


        public GetListReviewQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _sut = new GetListReviewQuery.GetListReviewQueryHandler(
                _reviewRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsReviewListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListReviewQuery>()
                .With(x => x.PageRequest, new PageRequest()
                {
                    Page = 0,
                    PageSize = 10
                })
                .Create();

            List<Domain.Entities.Review> existingReviews = _fixture.Build<Domain.Entities.Review>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Review>>();
            
            paginateMock.Setup(pag=>pag.Items).Returns(existingReviews);

            _reviewRepositoryMock.Setup(repo => repo.GetListAsync(
                null,null,It.IsAny<Func<IQueryable<Domain.Entities.Review>, IIncludableQueryable<Domain.Entities.Review, object>>>(),
                request.PageRequest.Page,request.PageRequest.PageSize,true,default
            )).ReturnsAsync(paginateMock.Object);
            
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ReviewListModel>(result);
            Assert.NotNull(result.Items);
            Assert.NotEmpty(result.Items);
            Assert.Equal(15, result.Items.Count);
            Assert.All(result.Items,item=>Assert.IsType<ReviewListDto>(item));
        }




    }
}
