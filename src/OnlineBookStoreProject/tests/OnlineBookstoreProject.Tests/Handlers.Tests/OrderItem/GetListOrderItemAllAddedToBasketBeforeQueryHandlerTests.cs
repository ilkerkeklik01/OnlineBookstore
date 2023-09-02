using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Application.Features.OrderItems.Queries.GetListOrderItemAllAddedToBasketBefore;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.OrderItems.Queries.GetListOrderItemAllAddedToBasketBefore.GetListOrderItemAllAddedToBasketBeforeQuery;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class GetListOrderItemAllAddedToBasketBeforeQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly GetListOrderItemAllAddedToBasketBeforeQueryHandler _sut;

        public GetListOrderItemAllAddedToBasketBeforeQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _sut = new GetListOrderItemAllAddedToBasketBeforeQueryHandler(
                _orderItemRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListOrderItemAllAddedToBasketBeforeQuery>()
                .With(x => x.PageRequest, new PageRequest()
                {
                    Page = 0,
                    PageSize = 10,
                })
                .With(x=>x.UserId,_fixture.Create<int>()+1)
                .Create();

            List<Domain.Entities.OrderItem> existingItems = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .With(x=>x.UserId,request.UserId)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(pag => pag.Items).Returns(existingItems);

            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.OrderItem, bool>>>(), null, It.IsAny<Func<IQueryable<Domain.Entities.OrderItem>, IIncludableQueryable<Domain.Entities.OrderItem, object>>>(),
                request.PageRequest.Page, request.PageRequest.PageSize, true, default
            )).ReturnsAsync(paginateMock.Object);

            var expectedModel = _mapper.Map<OrderItemListModel>(paginateMock.Object);
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.IsType<OrderItemListModel>(result);
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.NotEmpty(result.Items);
            Assert.Equal(15, result.Items.Count);
            Assert.All(result.Items,each=> Assert.IsType<OrderItemListDto>(each));
            Assert.All(result.Items,each=> Assert.Equal(request.UserId,each.UserId));
        }
        


    }
}
