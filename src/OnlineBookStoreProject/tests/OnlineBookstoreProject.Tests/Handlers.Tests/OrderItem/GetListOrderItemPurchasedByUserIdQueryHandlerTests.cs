using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItemInTheBasketByUserId;
using Application.Features.OrderItems.Queries.GetListOrderItemPurchasedByUserId;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class GetListOrderItemPurchasedByUserIdQueryHandlerTests
    {

        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly GetListOrderItemPurchasedByUserIdQuery
            .GetListOrderItemPurchasedByUserIdQueryHandler _sut;

        public GetListOrderItemPurchasedByUserIdQueryHandlerTests()
        {
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _sut = new GetListOrderItemPurchasedByUserIdQuery.GetListOrderItemPurchasedByUserIdQueryHandler(
                _orderItemRepositoryMock.Object, _mapper);
        }


        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnOrderItemListModelNotEmptyItems()
        {
            // Arrange
            var request = _fixture.Build<GetListOrderItemPurchasedByUserIdQuery>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .With(x => x.PageRequest, new PageRequest { Page = 0, PageSize = 10 })
                .Create();

            var existingOrderItems = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.IsInTheBasket, false)
                .CreateMany<Domain.Entities.OrderItem>(15)
                .ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(paginate => paginate.Items)
                .Returns(existingOrderItems);

            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                    It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                    null,
                    It.IsAny<Func<IQueryable<Domain.Entities.OrderItem>, IIncludableQueryable<Domain.Entities.OrderItem, object>>?>(),
                    request.PageRequest.Page,
                    request.PageRequest.PageSize,
                    true,
                    default
                ))
                .ReturnsAsync(paginateMock.Object);

            var expectedListModel = _mapper.Map<OrderItemListModel>(paginateMock.Object);

            // Act
            var result = await _sut.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.NotEmpty(result.Items);  // Change: Assert that the list is not empty
            Assert.Equal(expectedListModel.Items.Count, result.Items.Count);
        }


    }
}
