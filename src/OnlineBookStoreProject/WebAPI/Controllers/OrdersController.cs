using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
using Application.Features.Orders.Queries.GetListOrderByUserId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateOrderCommand command)
        {
            CreatedOrderDto createdOrderDto = await Mediator.Send(command);
            return Created("", createdOrderDto);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
        {
            OrderListModel orderListModel = await Mediator.Send(query);
            return Ok(orderListModel);
        }

        [HttpGet("getlistbyuserid")]
        public async Task<IActionResult> GetListByUserId([FromQuery] GetListOrderByUserIdQuery query)
        {
            OrderListModel orderListModel = await Mediator.Send(query);
            return Ok(orderListModel);
        }





    }




}
