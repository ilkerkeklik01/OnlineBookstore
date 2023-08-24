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
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class UpdateBookCommandHandlerTests
    {


        private readonly BookBusinessRules _rules;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly UpdateBookCommand.UpdateBookCommandHandler _sut;


        public UpdateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _orderItemRepositoryMock= new Mock<IOrderItemRepository>();
            _mapper = TestHelper.GetDefaultMapper();
            _rules = new BookBusinessRules(_bookRepositoryMock.Object); 
            _sut = new UpdateBookCommand.UpdateBookCommandHandler(
                _bookRepositoryMock.Object,
                _mapper,
                _rules,
                _orderItemRepositoryMock.Object
                );
        }

        [Fact]

        public async Task Handle_ValidRequest_ReturnsUpdatedDto()
        {
            //Arrange
            
            var request = new UpdateBookCommand()
            {
                Id = 1,
                Title = "Updated Title",
                Author = "Updated Author",
                CategoryId = 2,
                Description = "Updated Description",
                Price = 100,
                Discount = 20,
                PublicationDate = DateTime.Today.Subtract(TimeSpan.FromDays(10)),
                CoverImagePath = "updated/image/path"
            };

            var oldBook = new Domain.Entities.Book()
            {
                Id = 1,
                Title = "Old Title",
                Author = "Old Author",
                CategoryId = 1,
                Description = "Old Description",
                Price = 50,
                Discount = 10,
                PublicationDate = DateTime.Today,
                CoverImagePath = "old/image/path"
            };

            var updatedBook = new Domain.Entities.Book()
            {
                Id = 1,
                Title = "Updated Title",
                Author = "Updated Author",
                CategoryId = 2,
                Description = "Updated Description",
                Price = 100,
                Discount = 20,
                PublicationDate = DateTime.Now.Subtract(TimeSpan.FromDays(10)),
                CoverImagePath = "updated/image/path"
            };

            _bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.Is<Domain.Entities.Book>(b=>b.Id==1)))
                .ReturnsAsync(updatedBook);

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(oldBook);

            var expectedDto = _mapper.Map<UpdatedBookDto>(updatedBook);

            var paginateMock = new Mock<IPaginate<OrderItem>>();
            paginateMock.Setup(p => p.Items).Returns(new List<OrderItem>(){ new OrderItem { IsInTheBasket = true}});


            _orderItemRepositoryMock.Setup(repo => 
                repo.GetListAsync(It.IsAny<Expression<Func<OrderItem, bool>>>(),
                    null,null,0,10,true, default))
                .ReturnsAsync(paginateMock.Object);




            //Act
            var result = await _sut.Handle(request,CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UpdatedBookDto>(result);
            Assert.Equal(expected: expectedDto, actual: result);
        }




    }
}
