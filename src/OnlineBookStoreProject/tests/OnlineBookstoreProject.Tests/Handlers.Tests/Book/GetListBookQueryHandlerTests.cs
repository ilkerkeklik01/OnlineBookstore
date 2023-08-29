using System.Collections;
using System.Linq.Expressions;
using Application.Features.Books.Dtos;
using Application.Features.Books.Models;
using Application.Features.Books.Queries.GetListBook;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class GetListBookQueryHandlerTests
    {

        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetListBookQuery.GetListBookQueryHandler _sut;
        private readonly IFixture _fixture;

        public GetListBookQueryHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapper = TestHelper.Mapper;
            _sut = new GetListBookQuery.GetListBookQueryHandler(
                _bookRepositoryMock.Object,
                _mapper
                );
            _fixture = TestHelper.Fixture;
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsBookListModelItemsIsNotEmpty()
        {

            //Arrange
            var request = _fixture.Build<GetListBookQuery>()
                .With(x=>x.PageRequest,new PageRequest(){Page = 0,PageSize = 10})
                .Create();

            List<Domain.Entities.Book> existingBookList = _fixture.CreateMany<Domain.Entities.Book>(15).ToList();


            var paginateMock = new Mock<IPaginate<Domain.Entities.Book>>();
            paginateMock.Setup(p => p.Items).Returns(existingBookList);

            //existingBookList.Skip(page*pagesize).Take(pagesize).ToList() to test the pages


            _bookRepositoryMock.Setup(repo => repo.GetListAsync(
                    It.IsAny<Expression<Func<Domain.Entities.Book, bool>>>(),
                    null,null,request.PageRequest.Page, request.PageRequest.PageSize, true,default
                    ))
                .ReturnsAsync(paginateMock.Object);

            var expectedModel = _mapper.Map<BookListModel>(paginateMock.Object);
            
            //Act
            var result = await _sut.Handle(request,CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<BookListModel>(result);
            Assert.NotNull(result.Items);
            Assert.IsAssignableFrom<IList>(result.Items);
            Assert.NotEmpty(result.Items);
            Assert.IsType<BookListDto>(result.Items[0]);
            Assert.Equal(existingBookList.Count,result.Items.Count);
            Assert.Equal(_mapper.Map<BookListDto>(existingBookList[0]), result.Items[0]);
            
        }





    }
}
