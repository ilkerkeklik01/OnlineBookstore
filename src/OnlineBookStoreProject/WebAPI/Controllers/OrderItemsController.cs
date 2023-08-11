using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : BaseController
    {

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOrderItemCommand command)
        {
            CreatedOrderItemDto createdOrderItemDto = await Mediator.Send(command);
            return Created("", createdOrderItemDto);

        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderItemQuery query)
        {
            OrderItemListModel orderItemListModel = await Mediator.Send(query);
            return Ok(orderItemListModel);
        }



    }
}
