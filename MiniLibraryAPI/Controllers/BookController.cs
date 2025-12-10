using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Services;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Response<BookDto>> AddBook(CreateBookDto book)
    {
        var result = await _service.AddBook(book);
        return result;
    }
    
    [HttpPut]
    public async Task<Response<BookDto>> UpdateBook(BookDto book)
    { 
        var result = await _service.UpdateBook(book);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteBook(int id)
    {
        var result = await _service.DeleteBook(id);
        return result;
    }


    [HttpGet]
    public async Task<Response<PagedResponse<List<BookDto>>>> GetBooks([FromQuery] BookFilter filter)
    {
        var result = await _service.GetBooks(filter);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<BookDto>> GetBooksById(int id)
    {
        var result = await _service.GetBookById(id);
        return result;
    }    
}