using Application.Features.Bookshelves.Commands;
using Application.Features.Bookshelves.Dtos;
using Application.Features.Bookshelves.Models;
using Application.Features.Bookshelves.Queries.GetListBookshelf;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Models;
using Application.Features.Categories.Queries.GetListCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] GetListCategoryQuery getListCategoryQuery)
        {

            CategoryListModel result = await Mediator.Send(getListCategoryQuery);

            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreatedCategoryDto res = await Mediator.Send(createCategoryCommand);
            return Created("", res);
            
        }



    }
}
