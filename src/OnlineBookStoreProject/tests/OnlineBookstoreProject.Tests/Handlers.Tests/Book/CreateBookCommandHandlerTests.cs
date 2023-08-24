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
using AutoMapper;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class CreateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateBookCommand.CreateBookCommandHandler _sut;
        public CreateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapper = TestHelper.Mapper;
            _sut = new CreateBookCommand.CreateBookCommandHandler(
                _bookRepositoryMock.Object,
                _mapper,
                new BookBusinessRules(_bookRepositoryMock.Object)
                );
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedBookDto()
        {
            // Arrange
            var request = new CreateBookCommand
            { 
                Title = "Book Title",
                Author = "Book Author",
                CategoryId = 1,
                Discount = 10,
                Price = 50,
                Description = "Book Description",
                CoverImagePath = "path/to/book.jpg",
                PublicationDate = DateTime.Today
            };

            _bookRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Book>()))
                .ReturnsAsync((Domain.Entities.Book addedBook) =>
                {
                    addedBook.Id = 1;
                    return addedBook;
                });
            var expectedDto = new CreatedBookDto()
            {
                Id = 1,
                Title = "Book Title",
                Author = "Book Author",
                CategoryId = 1,
                Discount = 10,
                Price = 50,
                Description = "Book Description",
                CoverImagePath = "path/to/book.jpg",
                PublicationDate = DateTime.Today
            };

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedBookDto>(result);
            //Assert.Equal(createdBook.Id,result.Id);
            Assert.Equal(1,result.Id);
            Assert.Equal(expectedDto,result);//bu equal metotu neye gore calisiyor referans degil gibi duruyor
            

        }

    }
}
