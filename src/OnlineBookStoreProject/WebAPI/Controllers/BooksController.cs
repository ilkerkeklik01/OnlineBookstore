using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
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

            

    }
}
