using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Models;
using Application.Features.Reviews.Queries.GetListReview;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Models;
using Application.Features.Users.Queries.GetListUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand command)
        {
            CreatedUserDto createdUserDto = await Mediator.Send(command);
            return Created("", createdUserDto);
        }



        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListUserQuery query)
        {
            UserListModel userListModel = await Mediator.Send(query);
            return Ok(userListModel);
        }



    }
}
