﻿using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Dtos;
using Application.Features.Orders.Models;
using Application.Features.Orders.Queries.GetListOrder;
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







    }




}