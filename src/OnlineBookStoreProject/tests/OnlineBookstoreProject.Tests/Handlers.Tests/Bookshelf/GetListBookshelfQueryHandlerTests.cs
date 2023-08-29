using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Bookshelves.Models;
using Application.Features.Bookshelves.Queries.GetListBookshelf;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Bookshelf
{
    public class GetListBookshelfQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IBookshelfRepository> _bookshelfRepositoryMock;
        private readonly GetListBookshelfQuery.GetListBookshelfQueryHandler _sut;


        public GetListBookshelfQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _bookshelfRepositoryMock= new Mock<IBookshelfRepository>();
            _sut = new GetListBookshelfQuery.GetListBookshelfQueryHandler(
                _bookshelfRepositoryMock.Object,
                _mapper
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsBookshelfListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListBookshelfQuery>()
                .With(x => x.PageRequest, new PageRequest() { Page = 0, PageSize = 10 })
                .Create();

            List<Domain.Entities.Bookshelf> existingBookshelfs = _fixture.Build<Domain.Entities.Bookshelf>()
                .With(x => x.Id, _fixture.Create<int>() + 1).CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Bookshelf>>();

            paginateMock.Setup(paginate => paginate.Items).Returns(existingBookshelfs);

            _bookshelfRepositoryMock.Setup(repo => repo.GetListAsync(
                It.IsAny<Expression<Func<Domain.Entities.Bookshelf,bool>>>(),
                null, It.IsAny<Func<IQueryable<Domain.Entities.Bookshelf>, IIncludableQueryable<Domain.Entities.Bookshelf, object>>>()
                , request.PageRequest.Page,request.PageRequest.PageSize,
                true,default
            )).ReturnsAsync(paginateMock.Object);

            BookshelfListModel expectedResultModel = _mapper.Map<BookshelfListModel>(paginateMock.Object);

            //Act

            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.NotEmpty(result.Items);
            Assert.Equal(result.Items[2].Id, expectedResultModel.Items[2].Id);
            Assert.Equal(result.Items.Count, expectedResultModel.Items.Count);
        }

    }
}
