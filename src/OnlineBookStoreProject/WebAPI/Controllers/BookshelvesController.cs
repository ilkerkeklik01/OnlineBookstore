using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Bookshelves.Commands.CreateBook;
using Application.Features.Bookshelves.Dtos;
using Application.Features.Bookshelves.Models;
using Application.Features.Bookshelves.Queries.GetListBookshelf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookshelvesController : BaseController
    {


        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListBookshelfQuery query)
        {

            BookshelfListModel result = await Mediator.Send(query);

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateBookshelfCommand createBookshelfCommand)
        {
            CreatedBookshelfDto res = await Mediator.Send(createBookshelfCommand);
            return Created("", res);
        }


    }
}
