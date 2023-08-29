using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Dtos;
using Application.Features.Books.Models;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Models;
using Application.Features.Categories.Queries.GetListCategory;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Category
{
    public class GetListCategoryQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly GetListCategoryQuery.GetListCategoryQueryHandler _sut;

        public GetListCategoryQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _sut = new GetListCategoryQuery.GetListCategoryQueryHandler(
                _categoryRepositoryMock.Object,
                _mapper);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCategoryListModel()
        {
            //Arrange
            var request = _fixture.Build<GetListCategoryQuery>()
                .With(x => x.PageRequest, new PageRequest()
                {
                    Page = 0,
                    PageSize = 10,
                })
                .Create();

            List<Domain.Entities.Category> existingCategories = _fixture.Build<Domain.Entities.Category>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.Category>>();
            paginateMock.Setup(x => x.Items).Returns(existingCategories);

            _categoryRepositoryMock.Setup(repo => repo.GetListAsync(

                    It.IsAny<Expression<Func<Domain.Entities.Category, bool>>>(),
                    null, null, request.PageRequest.Page, request.PageRequest.PageSize, true, default)
            ).ReturnsAsync(paginateMock.Object);

            CategoryListModel expectedListModel = _mapper.Map<CategoryListModel>(paginateMock.Object);
            
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryListModel>(result);
            Assert.NotNull(result.Items);
            Assert.IsAssignableFrom<IList>(result.Items);
            Assert.NotEmpty(result.Items);
            Assert.IsType<CategoryListDto>(result.Items[0]);
            Assert.Equal(existingCategories.Count, result.Items.Count);
            Assert.Equal(existingCategories[7].Id, result.Items[7].Id);
        }




    }
}
