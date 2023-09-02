using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Order
{
    public class GetListOrderQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly GetListOrderQuery.GetListOrderQueryHandler _sut;

        public GetListOrderQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _sut = new GetListOrderQuery.GetListOrderQueryHandler(
                _mapper,
                _orderRepositoryMock.Object
            );

        }


        [Fact]

        public async Task Handle_ValidRequest_ReturnsOrderListModel()
        {

            //Arrange
            var request = _fixture.Build<GetListOrderQuery>()
                .With(x => x.PageRequest, new PageRequest() { Page = 0, PageSize = 10 })
                .Create();
            
            List<Domain.Entities.Order> existingOrders = _fixture.Build<Domain.Entities.Order>()
                .With(x=>x.Id,_fixture.Create<int>()+1)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Order>>();
            paginateMock.Setup(pag=>pag.Items).Returns(existingOrders);

            _orderRepositoryMock.Setup(repo => repo.GetListAsync(
                null,null,It.IsAny<Func<IQueryable<Domain.Entities.Order>, IIncludableQueryable<Domain.Entities.Order, object>>>(),
                request.PageRequest.Page,request.PageRequest.PageSize,true,default
            )).ReturnsAsync(paginateMock.Object);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderListModel>(result);
            Assert.NotNull(result.Items);
            Assert.Equal(15, result.Items.Count);
            Assert.All(result.Items,item=> Assert.IsType<OrderListDto>(item));
            
        }






    }
}
