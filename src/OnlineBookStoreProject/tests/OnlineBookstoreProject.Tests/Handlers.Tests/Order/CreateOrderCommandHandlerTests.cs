using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Order
{
    public class CreateOrderCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly OrderBusinessRules _orderBusinessRules;
        private readonly CreateOrderCommand.CreateOrderCommandHandler _sut;


        public CreateOrderCommandHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _orderBusinessRules = new OrderBusinessRules(_orderRepositoryMock.Object,
                 _orderItemRepositoryMock.Object, _userRepositoryMock.Object);
            _sut = new CreateOrderCommand.CreateOrderCommandHandler(
                _orderRepositoryMock.Object,_mapper,_userRepositoryMock.Object,
                _orderItemRepositoryMock.Object,_orderBusinessRules,_bookRepositoryMock.Object
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedOrderDto()
        {
            //Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id,request.UserId)
                .Create();
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            List<Domain.Entities.OrderItem> existingOrderItems = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x => x.IsInTheBasket, true)
                .With(x => x.UserId, request.UserId)
                .With(x=>x.BookPriceAtThatTime,100)
                .With(x=>x.BookDiscountAtThatTime,90)
                .With(x=>x.Quantity,10)
                .CreateMany(15).ToList();

            var orderItemPaginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            orderItemPaginateMock.Setup(pag => pag.Items).Returns(existingOrderItems);
            _orderItemRepositoryMock.Setup(repo=>repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                null,null,0,10,true,default
                )).ReturnsAsync(orderItemPaginateMock.Object);

            var expectedOrder = _fixture.Build<Domain.Entities.Order>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x => x.UserId, request.UserId)
                .With(x => x.User, user)
                .With(x => x.OrderItems, existingOrderItems)
                .With(x => x.OrderDate, DateTime.Now)
                .With(x => x.TotalPrice, 1500)
                .Create();
            _orderRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Order>()))
                .ReturnsAsync(expectedOrder);

            var book = _fixture.Build<Domain.Entities.Book>()
                .With(x => x.Discount, 90)
                .With(x => x.Price, 100)
                .Create();

            _bookRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>()))
                .ReturnsAsync(book);


            _orderItemRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.OrderItem>()))
                .Verifiable();


            //List<Domain.Entities.OrderItem> newItems = new List<Domain.Entities.OrderItem>();

            //existingOrderItems.ForEach(each =>
            //{
            //    each.IsInTheBasket = false;
            //    each.OrderId = expectedOrder.Id;
            //    _orderItemRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.OrderItem>()))
            //        .ReturnsAsync(each);
            //    newItems.Add(each);
            //});

            var expectedDto = _mapper.Map<CreatedOrderDto>(expectedOrder);
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedOrderDto>(result);
            Assert.Equal(expectedDto,result);
            Assert.All(result.OrderItems, item => Assert.False(item.IsInTheBasket));
            Assert.All(result.OrderItems, item => Assert.Equal(expectedOrder.Id,item.OrderId));
        }
        [Fact]
        public async Task Handle_InvalidRequestUserIsNull_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            Domain.Entities.User? user = null;
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);
            var expectedMessage = "User is not exist!";
            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () =>
                await _sut.Handle(request, CancellationToken.None)
            );
            Assert.Equal(expectedMessage,exception.Message);

        }



        [Fact]
        public async Task Handle_InvalidRequestBasketOfUserIsEmpty_ThrowsBusinessException()
        {
            //Arrange
            var request = _fixture.Build<CreateOrderCommand>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, request.UserId)
                .Create();
            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            List<Domain.Entities.OrderItem> existingOrderItems = new List<Domain.Entities.OrderItem>();

            var orderItemPaginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            orderItemPaginateMock.Setup(pag => pag.Items).Returns(existingOrderItems);
            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                null, null, 0, 10, true, default
                )).ReturnsAsync(orderItemPaginateMock.Object);

            var expectedMessage = "Basket of the user is empty!";
            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () => 
                await _sut.Handle(request,CancellationToken.None)
                );
            Assert.Equal(expectedMessage, exception.Message );
        }




    }
}
