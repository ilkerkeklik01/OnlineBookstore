using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Profiles;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateBookCommand.CreateBookCommandHandler _sut;
        private readonly IFixture _fixture;
        public CreateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapper = TestHelper.Mapper;
            _sut = new CreateBookCommand.CreateBookCommandHandler(
                _bookRepositoryMock.Object,
                _mapper,
                new BookBusinessRules(_bookRepositoryMock.Object)
                );
            _fixture = TestHelper.Fixture;
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedBookDto()
        {
            // Arrange
            var request = _fixture.Build<CreateBookCommand>()
                .With(x => x.CategoryId, _fixture.Create<int>() + 1)
                .Create();

            var createdBook = _fixture.Create<Domain.Entities.Book>();

            _bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Book>()))
                .ReturnsAsync(createdBook);

            var expectedDto = _mapper.Map<CreatedBookDto>(createdBook);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedBookDto>(result);
            Assert.Equal(expectedDto,result);
        }

    }
}
