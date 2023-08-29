using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Commands.DecreaseQuantityByOneOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class DecreaseQuantityByOneOrderItemCommandHandlerTests
    {

        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly OrderItemBusinessRules _ruleBusinessRules;
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly DecreaseQuantityByOneOrderItemCommand.DecreaseQuantityByOneOrderItemCommandHandler _sut;


        public DecreaseQuantityByOneOrderItemCommandHandlerTests()
        {
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _ruleBusinessRules = new OrderItemBusinessRules(_orderItemRepositoryMock.Object);
            _sut = new DecreaseQuantityByOneOrderItemCommand.DecreaseQuantityByOneOrderItemCommandHandler(
                _orderItemRepositoryMock.Object,
                _mapper,
                _bookRepositoryMock.Object,
                _userRepositoryMock.Object,
                _ruleBusinessRules
                );
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemDtoPassIfCondition()
        {
            //Arrange
            var request = _fixture.Build<DecreaseQuantityByOneOrderItemCommand>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .Create();

            var orderItem = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x=>x.Id,request.Id)
                .With(x => x.Quantity, 2)
                .With(x=>x.BookId,5)
                .With(x=>x.UserId,10)
                .With(x=>x.IsInTheBasket,true)
                .Create();
            int oldQuantity= orderItem.Quantity;
            _orderItemRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem,bool>>>()))
                .ReturnsAsync(orderItem);

            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Id, orderItem.BookId)
                .Create();
            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, orderItem.UserId)
                .Create();

            _bookRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.BookId))
                .ReturnsAsync(book);

            _userRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.UserId))
                .ReturnsAsync(user);


            _orderItemRepositoryMock.Setup(repo => repo.UpdateAsync(It.Is<Domain.Entities.OrderItem>(x=>x.Id==request.Id)))
                .ReturnsAsync(orderItem);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItemDto>(result);
            Assert.Equal(oldQuantity-1,result.Quantity);
            Assert.True(result.IsInTheBasket);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemDtoIfCondition()
        {
            //Arrange
            var request = _fixture.Build<DecreaseQuantityByOneOrderItemCommand>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .Create();

            var orderItem = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, request.Id)
                .With(x => x.Quantity, 1)
                .With(x => x.BookId, 5)
                .With(x => x.UserId, 10)
                .With(x => x.IsInTheBasket, true)
                .Create();
            int oldQuantity = orderItem.Quantity;
            _orderItemRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>()))
                .ReturnsAsync(orderItem);

            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Id, orderItem.BookId)
                .Create();
            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, orderItem.UserId)
                .Create();

            _bookRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.BookId))
                .ReturnsAsync(book);

            _userRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.UserId))
                .ReturnsAsync(user);



            _orderItemRepositoryMock.Setup(repo => repo.UpdateAsync(It.Is<Domain.Entities.OrderItem>(x => x.Id == request.Id)))
                .ReturnsAsync(orderItem);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderItemDto>(result);
            Assert.Equal(oldQuantity - 1, result.Quantity);
            Assert.False(result.IsInTheBasket);
        }


    }
}
