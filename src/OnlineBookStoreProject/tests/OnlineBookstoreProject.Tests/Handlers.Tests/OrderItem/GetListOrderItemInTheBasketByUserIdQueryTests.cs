using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItemInTheBasketByUserId;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class GetListOrderItemInTheBasketByUserIdQueryTests
    {
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;

        private readonly GetListOrderItemInTheBasketByUserIdQuery
            .GetListOrderItemInTheBasketByUserIdQueryHandler _sut;
        public GetListOrderItemInTheBasketByUserIdQueryTests()
        {
            _orderItemRepositoryMock =new Mock<IOrderItemRepository>();
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _sut = new GetListOrderItemInTheBasketByUserIdQuery
                .GetListOrderItemInTheBasketByUserIdQueryHandler(
                _orderItemRepositoryMock.Object,_mapper);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemListModelWithItems()
        {
            //Arrange
            var request = _fixture.Build<GetListOrderItemInTheBasketByUserIdQuery>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .With(x => x.PageRequest ,new PageRequest()
                    { Page = 0, PageSize = 10 })
                .Create();

            List<Domain.Entities.OrderItem> existingOrderItems = 
                _fixture.Build<Domain.Entities.OrderItem>()
                    .With(x=>x.IsInTheBasket,true)
                    .CreateMany<Domain.Entities.OrderItem>(15).ToList();


            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(paginate=>paginate.Items)
                .Returns(existingOrderItems);

            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.OrderItem,bool>>>(),
                null,It.IsAny<Func<IQueryable<Domain.Entities.OrderItem>, IIncludableQueryable<Domain.Entities.OrderItem, object>>?>(),
                request.PageRequest.Page,request.PageRequest.PageSize,true,default
            )).ReturnsAsync(paginateMock.Object);

            var expectedListModel = _mapper.Map<OrderItemListModel>(paginateMock.Object);
            
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.NotNull(result.Items[7]);
            Assert.Equal(expectedListModel.Items.Count, result.Items.Count);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemListModelEmptyItems()
        {
            //Arrange
            var request = _fixture.Build<GetListOrderItemInTheBasketByUserIdQuery>()
                .With(x => x.UserId, _fixture.Create<int>() + 1)
                .With(x => x.PageRequest, new PageRequest()
                    { Page = 0, PageSize = 10 })
                .Create();

            List<Domain.Entities.OrderItem> existingOrderItems =
                _fixture.Build<Domain.Entities.OrderItem>()
                    .With(x => x.IsInTheBasket, false)
                    .CreateMany<Domain.Entities.OrderItem>(15).ToList();


            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(paginate => paginate.Items)
                .Returns(existingOrderItems.Where(x=>x.IsInTheBasket).ToList());

            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(),
                null, It.IsAny<Func<IQueryable<Domain.Entities.OrderItem>, IIncludableQueryable<Domain.Entities.OrderItem, object>>?>(),
                request.PageRequest.Page, request.PageRequest.PageSize, true, default
            )).ReturnsAsync(paginateMock.Object);

            var expectedListModel = _mapper.Map<OrderItemListModel>(paginateMock.Object);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Empty(result.Items);
        }

    }
}
