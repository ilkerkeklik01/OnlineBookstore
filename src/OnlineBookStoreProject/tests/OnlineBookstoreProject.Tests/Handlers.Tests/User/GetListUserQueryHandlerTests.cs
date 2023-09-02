using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrderByUserId;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using Application.Features.Users.Queries.GetListUser;
using Application.Services.Repositories;
using AutoFixture;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Moq;

namespace OnlineBookstoreProject.Tests.Handlers.Tests.User
{
    public class GetListUserQueryHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly IFixture _fixture;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetListUserQuery.GetListUserQueryHandler _sut;

        public GetListUserQueryHandlerTests()
        {
            _mapper = TestHelper.Mapper;
            _fixture = TestHelper.Fixture;
            _userRepositoryMock= new Mock<IUserRepository>();
            _sut = new GetListUserQuery.GetListUserQueryHandler(
                _userRepositoryMock.Object,
                _mapper
                );
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsUserListModel()
        {

            //Arrange
            var request = _fixture.Build<GetListUserQuery>()
                .With(x => x.PageRequest, new PageRequest() { Page = 0, PageSize = 10 })
                .Create();


            List<Domain.Entities.User>  existingUsers = _fixture.Build<Domain.Entities.User>()
                .With(x => x.Id, _fixture.Create<int>() + 1)
                .CreateMany(15).ToList();

            var paginateMock = new Mock<IPaginate<Domain.Entities.User>>();
            paginateMock.Setup(pag => pag.Items).Returns(existingUsers);

            _userRepositoryMock.Setup(repo => repo.GetListAsync(
                null, null, null,
                request.PageRequest.Page, request.PageRequest.PageSize, true, default
            )).ReturnsAsync(paginateMock.Object);

            //Act
            var result = await _sut.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UserListModel>(result);
            Assert.NotNull(result.Items);
            Assert.Equal(15, result.Items.Count);
            Assert.All(result.Items, item => Assert.NotNull(item));
            Assert.All(result.Items, item => Assert.IsType<UserListDto>(item));

        }


    }
}
