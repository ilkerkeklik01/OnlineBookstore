using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Queries.GetListOrderByUserId;
using Application.Features.Orders.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using static Application.Features.Orders.Queries.GetListOrderByUserId.GetListOrderByUserIdQuery;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Order
{
    public class GetListOrderByUserIdQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetListOrderByUserIdQueryHandler _sut;
        private readonly OrderBusinessRules _orderBusinessRules;
        public GetListOrderByUserIdQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _orderBusinessRules = new OrderBusinessRules(_orderRepositoryMock.Object
            , _orderItemRepositoryMock.Object
            , _userRepositoryMock.Object
            );
            _sut = new GetListOrderByUserIdQuery.GetListOrderByUserIdQueryHandler(
                _mapper,
                _orderRepositoryMock.Object,
                _orderBusinessRules
            );
        }


        [Fact]

        public async Task Handle_ValidRequest_ReturnsOrderListModel()
        {

            //Arrange
            var request = _fixture.Build<GetListOrderByUserIdQuery>()
                .With(x => x.PageRequest, new PageRequest() { Page = 0, PageSize = 10 })
                .With(x => x.UserId,_fixture.Create<int>()+1)
                .Create();

            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, request.UserId)
                .Create();

            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);


            List<Domain.Entities.Order> existingOrders = _fixture.Build<Domain.Entities.Order>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x=>x.UserId,request.UserId)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Order>>();
            paginateMock.Setup(pag => pag.Items).Returns(existingOrders);

            _orderRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.Order,bool>>>(), null, It.IsAny<Func<IQueryable<Domain.Entities.Order>, IIncludableQueryable<Domain.Entities.Order, object>>>(),
                request.PageRequest.Page, request.PageRequest.PageSize, true, default
            )).ReturnsAsync(paginateMock.Object);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderListModel>(result);
            Assert.NotNull(result.Items);
            Assert.Equal(15, result.Items.Count);
            Assert.All(result.Items, item => Assert.IsType<OrderListDto>(item));
            Assert.All(result.Items, item => Assert.Equal(request.UserId,item.UserId));

        }
        [Fact]

        public async Task Handle_InvalidRequest_ThrowsBusinessException()
        {

            //Arrange
            var request = _fixture.Build<GetListOrderByUserIdQuery>()
                .With(x => x.PageRequest, new PageRequest() { Page = 0, PageSize = 10 })
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .Create();

            Domain.Entities.User? user = null;

            _userRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.User, bool>>>()))
                .ReturnsAsync(user);

            var expectedMessage = "User is not exist!";
            //Act and Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _sut.Handle(request, CancellationToken.None));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
