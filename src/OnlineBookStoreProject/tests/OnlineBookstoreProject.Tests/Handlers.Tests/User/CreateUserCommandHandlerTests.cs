using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.User
{
    public class CreateUserCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly CreateUserCommand.CreateUserCommandHandler _sut;

        public CreateUserCommandHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _userRepositoryMock = new Mock<IUserRepository>();
            _sut = new CreateUserCommand.CreateUserCommandHandler(
                _userRepositoryMock.Object,_mapper
                );
        }


        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreatedUserDto()
        {
            //Arrange
            var request = _fixture.Create<CreateUserCommand>();

            var user = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, _fixture.Create<int>())
                .With(x => x.Email, request.Email)
                .With(x => x.Password, request.Password)
                .With(x => x.Username, request.Username)
                .With(x => x.PasswordUpdatedAt, DateTime.Now)
                .With(x => x.RegistrationDate, DateTime.Now)
                .Create();

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.User>()))
                .ReturnsAsync(user);
            var expectedDto = _mapper.Map<CreatedUserDto>(user);
            //Act
            var result = await _sut.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedUserDto>(result);
            Assert.Equal(expectedDto,result);
        }



    }
}
