using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using AutoFixture;


namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class CreateBookCommandValidatorTests
    {

        private readonly CreateBookCommandValidator _sut;
        private readonly IFixture _fixture;
        public CreateBookCommandValidatorTests()
        {
            _sut = new CreateBookCommandValidator();
            _fixture = TestHelper.Fixture;
        }

        [Fact]

        public void Should_Have_Error_When_Author_Is_Null()
        {
            //Arrange
            var command = _fixture.Create<CreateBookCommand>();
            command.Author = null;

            //Act
            var result = _sut.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "NotNullValidator" && error.PropertyName == "Author");
        }


        [Fact]
        public void Should_Have_Error_When_Author_Is_Empty()
        {
            //Arrange
            var command = _fixture.Create<CreateBookCommand>();
            command.Author = "";
            //Act
            var result = _sut.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "NotEmptyValidator" && error.PropertyName == "Author");
        }

        [Fact]
        public void Should_Have_Error_When_Author_Is_Short()
        {
            //Arrange
            var command = _fixture.Create<CreateBookCommand>();
            command.Author = "xy";
            //Act
            var result = _sut.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "MinimumLengthValidator" && error.PropertyName == "Author");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Null()
        {
            //Arrange
            var command = _fixture.Create<CreateBookCommand>();
            command.Title = null;
            //Act
            var result = _sut.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "NotNullValidator" && error.PropertyName == "Title");
        }
        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            //Arrange
            var command = _fixture.Create<CreateBookCommand>();
            command.Title = "";
            //Act
            var result = _sut.Validate(command);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "NotEmptyValidator" && error.PropertyName == "Title");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Greater_Than_100()
        {
            //Arrange 
            var command = _fixture.Create<CreateBookCommand>();
            command.Price = 101;
            //Act
            var result = _sut.Validate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "LessThanOrEqualValidator" && error.PropertyName == "Price");
        }
        [Fact]
        public void Should_Have_Error_When_Price_Less_Than_1()
        {
            //Arrange 
            var command = _fixture.Create<CreateBookCommand>();
            command.Price = 0;
            //Act
            var result = _sut.Validate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error =>
                error.ErrorCode == "GreaterThanOrEqualValidator" && error.PropertyName == "Price");
        }

        [Fact]
        public void Should_Not_Have_Error_When_All_Properties_Are_Valid()
        {
            //Arrange
            var command = new CreateBookCommand
            {
                Author = "Valid Author",
                Title = "Valid Title",
                Price = 50
            };
            //Assert
            var result = _sut.Validate(command);
            //Act
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

    }
}
