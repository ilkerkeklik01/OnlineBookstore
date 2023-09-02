using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Review
{
    public class CreateReviewCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly CreateReviewCommand.CreateReviewCommandHandler _sut;
        private readonly ReviewBusinessRules _reviewBusinessRules;
        public CreateReviewCommandHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _bookRepositoryMock = new Mock<IBookRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _reviewBusinessRules = new ReviewBusinessRules(_userRepositoryMock.Object,
                _reviewRepositoryMock.Object,_bookRepositoryMock.Object
                );
            _sut = new CreateReviewCommand.CreateReviewCommandHandler(
                _reviewRepositoryMock.Object,_mapper,_userRepositoryMock.Object,
                _bookRepositoryMock.Object,_reviewBusinessRules
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedReviewDto()
        {
            //Arrange
            var request = _fixture.Build<CreateReviewCommand>()
                .With(x => x.BookId, _fixture.Create<int>() + 1)
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();
            
            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, request.UserId)
                .Create();
            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Id, request.BookId)
                .Create();
            _bookRepositoryMock.Setup(repo =>
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(book);
            _userRepositoryMock.Setup(repo =>
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            var review = _fixture.Build<Domain.Entities.Review>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x => x.BookId, request.BookId)
                .With(x => x.UserId, request.UserId)
                .With(x => x.CreatedAt, DateTime.Now)
                .Create();
            
            review.Book=book;
            review.User=user;

            _reviewRepositoryMock.Setup(repo=>
                repo.AddAsync(It.IsAny<Domain.Entities.Review>()))
                .ReturnsAsync(review);

            var expectedDto = _mapper.Map<CreatedReviewDto>(review);
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedReviewDto>(result);
            Assert.Equal(expectedDto,result);
        }


        [Fact]
        public async Task Handle_InvalidRequestUserIsNull_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateReviewCommand>()
                .With(x => x.BookId, _fixture.Create<int>() + 1)
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            Domain.Entities.User? user = null;
            _userRepositoryMock.Setup(repo =>
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            var expectedMessage = "User is not exist!";
            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
                await _sut.Handle(request,CancellationToken.None)
                );
            Assert.Equal(expectedMessage, exception.Message);

        }

        [Fact]
        public async Task Handle_InvalidRequestBookIsNull_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateReviewCommand>()
                .With(x => x.BookId, _fixture.Create<int>() + 1)
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, request.UserId)
                .Create();
            _userRepositoryMock.Setup(repo =>
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            Domain.Entities.Book? book = null;
            _bookRepositoryMock.Setup(repo =>
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(book);

            var expectedMessage = "Book is not exist (null)";
            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
                await _sut.Handle(request, CancellationToken.None));
            Assert.Equal(expectedMessage, exception.Message);
        }

    }
}
