using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Models;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Queries.GetListBook;
using Application.Features.Books.Queries.GetListBookByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController
    {
        [HttpPost("add")]
        
        public async Task<IActionResult> Add([FromBody] CreateBookCommand createBookCommand)
        {
            CreatedBookDto res = await Mediator.Send(createBookCommand);
            return Created("", res);
        }


        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListBookQuery getListBookQuery)
        {
            BookListModel result = await Mediator.Send(getListBookQuery);   

            return Ok(result);

        }

        [HttpGet("getbyid{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdBookQuery getListBookQuery)
        {
            BookDto result = await Mediator.Send(getListBookQuery);

            return Ok(result);

        }

        [HttpPost("getlist/bydynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest request
        ,[FromBody] Dynamic dynamic)
        {
            GetListBookByDynamicQuery getListBookByDynamicQuery = new() {PageRequest = request,Dynamic = dynamic};
            BookListModel result = await Mediator.Send(getListBookByDynamicQuery);

            return Ok(result);

        }






    }
}
