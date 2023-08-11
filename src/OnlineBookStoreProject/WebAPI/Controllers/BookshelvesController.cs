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


    }
}
