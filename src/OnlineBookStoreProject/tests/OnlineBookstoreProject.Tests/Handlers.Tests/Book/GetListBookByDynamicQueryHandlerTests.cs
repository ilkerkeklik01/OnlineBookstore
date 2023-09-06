using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Models;
using Application.Features.Books.Queries.GetListBookByDynamic;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class GetListBookByDynamicQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly GetListBookByDynamicQuery.GetListBookByDynamicQueryHandler _sut;

        public GetListBookByDynamicQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _bookRepositoryMock = new Mock<IBookRepository>();
            _sut = new GetListBookByDynamicQuery.GetListBookByDynamicQueryHandler(
                _bookRepositoryMock.Object,
                _mapper
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsBookListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListBookByDynamicQuery>()
                .With(x=>x.PageRequest,new PageRequest(){Page = 0,PageSize = 10})
                .With(x=>x.Dynamic,new Dynamic(){})
                .Create();

            List<Domain.Entities.Book> existingList = _fixture.CreateMany<Domain.Entities.Book>(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Book>>();
            paginateMock.Setup(pag => pag.Items).Returns(existingList);

            _bookRepositoryMock.Setup(repo => repo.GetListByDynamicAsync(
                It.IsAny<Dynamic>(),null,request.PageRequest.Page,request.PageRequest.PageSize,
                true,default
            )).ReturnsAsync(paginateMock.Object);

            BookListModel expectedModel = _mapper.Map<BookListModel>(paginateMock.Object);
            
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BookListModel>(result);
            Assert.Equal(15, result.Items.Count);
            Assert.NotNull(result.Items);
            Assert.Equal(expectedModel.Items,result.Items);
        }



    }
}
