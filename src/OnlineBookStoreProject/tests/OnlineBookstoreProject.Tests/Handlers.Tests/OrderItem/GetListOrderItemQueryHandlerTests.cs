using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Application.Features.Orders.Models;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.OrderItem
{
    public class GetListOrderItemQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IOrderItemRepository> _orderItemRepositoryMock;
        private readonly GetListOrderItemQuery.GetListOrderItemQueryHandler _sut;

        public GetListOrderItemQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            _sut = new GetListOrderItemQuery.GetListOrderItemQueryHandler(
                _orderItemRepositoryMock.Object,
                _mapper
            );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsOrderItemListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListOrderItemQuery>()
                .With(x => x.PageRequest, new PageRequest()
                {
                    Page = 0,
                    PageSize = 10,
                })
                .Create();

            List<Domain.Entities.OrderItem> existingItems = _fixture.Build<Domain.Entities.OrderItem>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.OrderItem>>();
            paginateMock.Setup(pag=>pag.Items).Returns(existingItems);

            _orderItemRepositoryMock.Setup(repo => repo.GetListAsync(
                null,null,It.IsAny<Func<IQueryable<Domain.Entities.OrderItem>, IIncludableQueryable<Domain.Entities.OrderItem, object>>>(),
                request.PageRequest.Page,request.PageRequest.PageSize,true,default
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
            Assert.IsType<OrderItemListDto>(result.Items[3]);
        }



    }
}
