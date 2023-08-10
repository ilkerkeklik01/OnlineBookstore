﻿using Application.Features.Books.Commands.CreateBook;
using Application.Features.Books.Dtos;
using Application.Features.Books.Models;
using Application.Features.Books.Queries.GetByIdBook;
using Application.Features.Books.Queries.GetListBook;
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
        public async Task<IActionResult> GetList([FromRoute] GetByIdBookQuery getListBookQuery)
        {
            BookDto result = await Mediator.Send(getListBookQuery);

            return Ok(result);

        }





    }
}
