using System.Linq.Expressions;
using Application.Features.OrderItems.Commands.IncreaseQuantityByOneOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class IncreaseQuantityByOneOrderItemCommandHandlerTests
    {

        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly OrderItemBusinessRules _orderItemBusinessRules;
        private readonly IncreaseQuantityByOneOrderItemCommand.IncreaseQuantityByOneOrderItemCommandHandler _sut;


        public IncreaseQuantityByOneOrderItemCommandHandlerTests()
        {
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderItemBusinessRules = new OrderItemBusinessRules(_orderItemRepositoryMock.Object);
            _sut = new IncreaseQuantityByOneOrderItemCommand.IncreaseQuantityByOneOrderItemCommandHandler(
                _orderItemRepositoryMock.Object,
                _mapper,
                _bookRepositoryMock.Object,
                _userRepositoryMock.Object,
                _orderItemBusinessRules
                );
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemDto()
        {
            var request = _fixture.Build<IncreaseQuantityByOneOrderItemCommand>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .Create();

            var orderItem = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, request.Id)
                .With(x=>x.Quantity,9)
                .With(x=>x.IsInTheBasket,true)
                .Create();

            _orderItemRepositoryMock.Setup(repo=>
                repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.OrderItem,bool>>>()))
                .ReturnsAsync(orderItem);
            //bu setup kesin calısmayacak iyi izle

            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Id, orderItem.BookId)
                .Create();
            var user = _fixture.Build<User>()
                .With(x => x.Id, orderItem.UserId)
                .Create();
            _bookRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.BookId))
                .ReturnsAsync(book);
            _userRepositoryMock.Setup(repo => repo.GetAsync(x => x.Id == orderItem.UserId))
                .ReturnsAsync(user);
            //Bunlar da patlatacak...


            _orderItemRepositoryMock.Setup(repo => repo.UpdateAsync(
                It.IsAny<Domain.Entities.OrderItem>()
            )).ReturnsAsync(orderItem);

            //Act
            var result =await _sut.Handle(request, CancellationToken.None);

            //Assert 
            Assert.NotNull(result);
            Assert.IsType<OrderItemDto>(result);
            Assert.Equal(10, result.Quantity);
            Assert.Equal(result.BookId,book.Id);
        }
    }
}
