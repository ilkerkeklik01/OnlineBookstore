using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class CreateOrderItemCommandHandlerTests
    {
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly OrderItemBusinessRules _orderItemBusinessRules;
        private readonly CreateOrderItemCommand.CreateOrderItemCommandHandler _sut;
        private readonly IFixture _fixture;
        public CreateOrderItemCommandHandlerTests()
        {
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _orderItemBusinessRules = new OrderItemBusinessRules(_orderItemRepositoryMock.Object);
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _sut = new CreateOrderItemCommand.CreateOrderItemCommandHandler(
                _orderItemRepositoryMock.Object,_mapper,_bookRepositoryMock.Object,
                _userRepositoryMock.Object,_orderItemBusinessRules
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedOrderItemDto()
        {

            //Arrange
            var request = _fixture.Build<CreateOrderItemCommand>()
                .With(x => x.BookId, _fixture.Create<int>() + 1)
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            var orderItemsPaginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();

            List<Domain.Entities.OrderItem> existingOrderItems = new List<Domain.Entities.OrderItem>();

            orderItemsPaginateMock.Setup(p => p.Items)
                .Returns(existingOrderItems);
            
            _orderItemRepositoryMock.Setup(repo =>
                    repo.GetListAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                        null, null, 0, 10, true, default
                    ))
                .ReturnsAsync(orderItemsPaginateMock.Object);

            var mappedOrderItem = _mapper.Map<Domain.Entities.OrderItem>(request);
            mappedOrderItem.IsInTheBasket=true;
            mappedOrderItem.Quantity = 1;
            
            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Id, request.BookId)
                .Create();
            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, request.UserId)
                .Create();

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book,bool>>>()))
                .ReturnsAsync(book);
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            mappedOrderItem.Book=book;
            mappedOrderItem.User=user;


            _mapper.Map(mappedOrderItem.Book, mappedOrderItem);

            var createdOrderItem = mappedOrderItem;
            createdOrderItem.Id = _fixture.Create<int>() + 1;

            _orderItemRepositoryMock.Setup(repo => repo.AddAsync(It.Is<Domain.Entities.OrderItem>
                    (x => x.BookId == request.BookId && x.UserId == request.UserId)))
                .ReturnsAsync(createdOrderItem);

            var expectedCreatedOrderItemDto = _mapper.Map<CreatedOrderItemDto>(createdOrderItem);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedOrderItemDto>(result);
            Assert.Equal(expectedCreatedOrderItemDto,result);
        }

        [Fact]
        public async Task Handle_InvalidRequestOrderItemExists_ThrowsBussinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateOrderItemCommand>()
                .With(x => x.BookId, _fixture.Create<int>() + 1)
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            var orderItemsPaginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();

            List<Domain.Entities.OrderItem> existingOrderItems = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .CreateMany(15).ToList();

            orderItemsPaginateMock.Setup(p => p.Items)
                .Returns(existingOrderItems);

            _orderItemRepositoryMock.Setup(repo =>
                    repo.GetListAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                        null, null, 0, 10, true, default
                    ))
                .ReturnsAsync(orderItemsPaginateMock.Object);

            var expectedMessage = "You cannot create an order item with existing book if it is in the basket of the same user! Try to increase quantity.";

            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
                await _sut.Handle(request,CancellationToken.None));
            Assert.Equal(expectedMessage, exception.Message);
        }




    }
}
