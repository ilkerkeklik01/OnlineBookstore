using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Dtos;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Rules;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.Book
{
    public class GetByIdBookQueryHandlerTests
    {

        private readonly IFixture _fixture;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly IMapper _mapper;
        private readonly BookBusinessRules _rules;
        private readonly GetByIdBookQuery.GetByIdBookQueryHandler _sut;


        public GetByIdBookQueryHandlerTests()
        {
            _fixture = TestHelper.Fixture;
            _bookRepositoryMock = new Mock<IBookRepository>();
            _mapper = TestHelper.Mapper;
            _rules = new BookBusinessRules(_bookRepositoryMock.Object);
            _sut = new GetByIdBookQuery.GetByIdBookQueryHandler(_bookRepositoryMock.Object,
                _mapper,
                _rules
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsBookDto()
        {

            //Arrange
            var request = _fixture.Build<GetByIdBookQuery>()
                .With(r => r.Id, _fixture.Create<int>() + 1)
                .Create();



            var existingBook = _fixture.Create<Domain.Entities.Book>();

            _bookRepositoryMock.Setup(repo => 
                    repo.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Book,bool>>>())
                    )
                .ReturnsAsync(existingBook);

            var expectedDto = _mapper.Map<BookDto>(existingBook);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            
            //Assert
            Assert.IsType<BookDto>(result);
            Assert.NotNull(result);
            Assert.Equal(expectedDto,result);
        }


    }
}
