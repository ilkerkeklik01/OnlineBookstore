using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly UpdateBookCommand.UpdateBookCommandHandler _sut;
        private readonly IFixture _fixture;

        public UpdateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _orderItemRepositoryMock= new Mock<IOrderItemRepository>();
            _mapper = TestHelper.Mapper;
            var rules = new BookBusinessRules(_bookRepositoryMock.Object); 
            _sut = new UpdateBookCommand.UpdateBookCommandHandler(
                _bookRepositoryMock.Object,
                _mapper,
                rules,
                _orderItemRepositoryMock.Object
                );
            _fixture = TestHelper.Fixture;
        }

        [Fact]

        public async Task Handle_ValidRequest_ReturnsUpdatedDto()
        {
            //Arrange
            var request = _fixture.Build<UpdateBookCommand>()
                .With(x => x.Id, 1)
                .With(x => x.Price, 100)
                .With(x => x.CategoryId, 2)
                .With(x => x.Author, "abs")
                .With(x => x.Title, "klm")
                .Create();
                            
            
            var oldBook = _fixture.Build<Domain.Entities.Book>()
                .With(x=>x.Id,request.Id)
                .Create();


            var updatedBook = _mapper.Map<Domain.Entities.Book>(request);

            _bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.Is<Domain.Entities.Book>(b=>b.Id==1)))
                .ReturnsAsync(updatedBook);

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(oldBook);

            var expectedDto = _mapper.Map<UpdatedBookDto>(updatedBook);

            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(p => p.Items).Returns(new List<Domain.Entities.OrderItem>(){ new Domain.Entities.OrderItem { IsInTheBasket = true}});


            _orderItemRepositoryMock.Setup(repo => 
                repo.GetListAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                    null,null,0,10,true, default))
                .ReturnsAsync(paginateMock.Object);

            //Act
            var result = await _sut.Handle(request,CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UpdatedBookDto>(result);
            Assert.Equal(expected: expectedDto, actual: result);
        }

        [Fact]

        public async Task Handle_InvalidIdRequestBookIsNull_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<UpdateBookCommand>()
                .With(x => x.Id, 1)
                .Create();

            Domain.Entities.Book oldBook = null;

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(oldBook);
            var expectedMessage = "Book is not exist (null)";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _sut.Handle(request, CancellationToken.None));
            Assert.Equal(expectedMessage,exception.Message);
        }




    }
}
