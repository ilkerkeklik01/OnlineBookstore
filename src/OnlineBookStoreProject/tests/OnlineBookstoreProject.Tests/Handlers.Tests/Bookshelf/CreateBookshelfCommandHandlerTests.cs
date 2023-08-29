using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Bookshelves.Commands.CreateBookshelf;
using Application.Features.Bookshelves.Dtos;
using Application.Features.Bookshelves.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Bookshelf
{
    public class CreateBookshelfCommandHandlerTests
    {

        private readonly IFixture _fixture;
        private readonly Mock<IBookshelfRepository> _bookshelfRepositoryMock;
        private readonly IMapper _mapper;
        private readonly BookshelfBusinessRules _bookshelfBusinessRules;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly CreateBookshelfCommand.CreateBookshelfCommandHandler _sut;
        public CreateBookshelfCommandHandlerTests()
        {
            _fixture = TestHelper.Fixture;
            _mapper = TestHelper.Mapper;
            _bookshelfRepositoryMock = new Mock<IBookshelfRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _bookshelfBusinessRules = new BookshelfBusinessRules(_userRepositoryMock.Object);
            _sut = new CreateBookshelfCommand.CreateBookshelfCommandHandler(
                _bookshelfRepositoryMock.Object,
                _mapper,
                _userRepositoryMock.Object,
                _bookshelfBusinessRules
                );
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedBookshelfDto()
        {
            //Arrange
            var request = _fixture.Build<CreateBookshelfCommand>()
                .With(x => x.Name, _fixture.Create<string>())
                .With(x => x.UserId,_fixture.Create<int>()+1)
                .Create();

            var user = _fixture.Build<User>()
                .With(x => x.Id, request.UserId)
                .With(x => x.Username, _fixture.Create<string>())
                .Create();

            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny < Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);

            var mappedBookshelf = _mapper.Map<Domain.Entities.Bookshelf>(request);
            mappedBookshelf.User = user;

            var createdBookshelf = _fixture.Build<Domain.Entities.Bookshelf>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x => x.Name, mappedBookshelf.Name)
                .With(x => x.UserId, mappedBookshelf.UserId)
                .With(x => x.User, mappedBookshelf.User)
                .Create();

            _bookshelfRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Bookshelf>()))
                .ReturnsAsync(createdBookshelf);
            var expectedDto = _mapper.Map<CreatedBookshelfDto>(createdBookshelf);
            
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedBookshelfDto>(result);
            Assert.Equal(expectedDto,result);
            Assert.Equal(createdBookshelf.User.Username,result.UserName);
        }


        [Fact]
        public async Task Handle_UserIsNotExist_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateBookshelfCommand>()
                .With(x => x.Name, _fixture.Create<string>())
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            User? user = null;

            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(user);
            var expectedMessage = "User is not exist!";
            // Act & Assert

            var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _sut.Handle(request, CancellationToken.None));
            Assert.Equal(expectedMessage, exception.Message);

        }

    }
}
