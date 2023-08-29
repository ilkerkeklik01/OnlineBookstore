using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.UpdateBook;
using Application.Features.Books.Queries.GetByIdBook;
using AutoFixture;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class GetByIdBookQueryValidatorTests
    {
        private readonly IFixture _fixture;
        private readonly GetByIdBookQueryValidator _sut;

        public GetByIdBookQueryValidatorTests()
        {
            _fixture = TestHelper.Fixture;
            _sut = new GetByIdBookQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Less_Then_1()
        {
            //Arrange
            var command = _fixture.Build<GetByIdBookQuery>()
                .With(x => x.Id,-_fixture.Create<int>())
                .Create();
            //Act
            var result = _sut.Validate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "PredicateValidator" && error.PropertyName == "Id");
        }
        [Fact]
        public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
        {
            //Arrange
            var command = new GetByIdBookQuery()
            {
                Id = 1
            };
            //Act
            var result = _sut.Validate(command);
            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

    }
}
