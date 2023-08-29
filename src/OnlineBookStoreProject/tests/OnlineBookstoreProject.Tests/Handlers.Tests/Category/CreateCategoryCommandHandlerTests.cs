using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Category
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly CreateCategoryCommand.CreateCategoryCommandHandler _sut;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        public CreateCategoryCommandHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _sut = new CreateCategoryCommand.CreateCategoryCommandHandler(
                _categoryRepositoryMock.Object,
                _mapper
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedCategoryDto()
        {
            //Arrange
            var request = _fixture.Build<CreateCategoryCommand>()
                .With(x => x.Name, "Category Name")
                .Create();

            var createdCategory = _mapper.Map<Domain.Entities.Category>(request);
            createdCategory.Id = _fixture.Create<int>() + 1;
            _categoryRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Category>()))
                .ReturnsAsync(createdCategory);

            var expectedDto = _mapper.Map<CreatedCategoryDto>(createdCategory);
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.IsType<CreatedCategoryDto>(result);
            Assert.NotNull(result);
            Assert.Equal(createdCategory.Id,result.Id);
            Assert.Equal(createdCategory.Name,result.Name);
        }


    }
}
