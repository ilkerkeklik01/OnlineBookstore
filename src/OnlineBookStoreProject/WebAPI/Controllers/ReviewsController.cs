using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
using Application.Features.Reviews.Commands.CreateReview;
using Application.Features.Reviews.Dtos;
using Application.Features.Reviews.Models;
using Application.Features.Reviews.Queries.GetListReview;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : BaseController
    {
        

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateReviewCommand command)
        {
            CreatedReviewDto createdReviewDto = await Mediator.Send(command);
            return Created("", createdReviewDto);

        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListReviewQuery query)
        {
            ReviewListModel reviewListModel = await Mediator.Send(query);
            return Ok(reviewListModel);
        }




    }
}
