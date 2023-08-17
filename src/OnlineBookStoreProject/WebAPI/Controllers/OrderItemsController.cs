using Application.Features.OrderItems.Commands.CreateOrderItem;
using Application.Features.OrderItems.Commands.DecreaseQuantityByOneOrderItem;
using Application.Features.OrderItems.Commands.IncreaseQuantityByOneOrderItem;
using Application.Features.OrderItems.Dtos;
using Application.Features.OrderItems.Models;
using Application.Features.OrderItems.Queries.GetListOrderItem;
using Application.Features.OrderItems.Queries.GetListOrderItemAddedRemovedButNotPurchasedByUserId;
using Application.Features.OrderItems.Queries.GetListOrderItemAddedRemovedButNotPurchasedByUserId;
using Application.Features.OrderItems.Queries.GetListOrderItemAllAddedToBasketBefore;
using Application.Features.OrderItems.Queries.GetListOrderItemInTheBasketByUserId;
using Application.Features.OrderItems.Queries.GetListOrderItemPurchasedByUserId;
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

        [HttpPost("increase={Id}")]
        public async Task<IActionResult> IncreaseQuantityOfOrderItem(
            [FromRoute] IncreaseQuantityByOneOrderItemCommand command)
        {
            OrderItemDto orderItemDto = await Mediator.Send(command);
            return Ok(orderItemDto);
        }

        [HttpPost("decrease={Id}")]

        public async Task<IActionResult> DecreaseQuantityOfOrderItem(
            [FromRoute] DecreaseQuantityByOneOrderItemCommand command
        )
        {
            OrderItemDto orderItemDto = await Mediator.Send(command);

            return Ok(orderItemDto);
        }

        [HttpGet("getlistbasket")]
        public async Task<IActionResult> GetListBasket([FromQuery] GetListOrderItemInTheBasketByUserIdQuery query)
        {
            OrderItemListModel orderItemListModel = await Mediator.Send(query);
            return Ok(orderItemListModel);
        }

        [HttpGet("getlistpurchased")]
        public async Task<IActionResult> GetListPurchased( [FromQuery] GetListOrderItemPurchasedByUserIdQuery query)
        {
            OrderItemListModel orderItemListModel = await Mediator.Send(query);
            return Ok(orderItemListModel);
        }

        [HttpGet("getlistaddedremovednotpurchased")]
        public async Task<IActionResult> GetListAddedNotPurchased([FromQuery] GetListOrderItemAddedRemovedButNotPurchasedByUserIdQuery query)
        {
            OrderItemListModel orderItemListModel = await Mediator.Send(query);
            return Ok(orderItemListModel);
        }
        [HttpGet("getlistalladdedtobasketbefore")]
        public async Task<IActionResult> GetListOrderItemAllAddedToBasketBefore( [FromQuery] GetListOrderItemAllAddedToBasketBeforeQuery query)
        {
            OrderItemListModel orderItemListModel = await Mediator.Send(query);
            return Ok(orderItemListModel);
        }




    }
}
