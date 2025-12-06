using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService _service) : ControllerBase
{
    [HttpPost]
    public async Task<BookDto> AddBook(CreateBookDto book)
    {
        var createdBook = await _service.AddBook(book);
        return createdBook;
    }
    
    [HttpPut]
    public async Task<Book> UpdateBook(BookDto book)
    { 
        var updateBook = await _service.UpdateBook(book);
        return updateBook;
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteBook(int id)
    {
        var result = await _service.DeleteBook(id);
        if(result>0)
        {
            return "Delete Succefully!";
        }

        return "Deleted not succefully!";
    }


    [HttpGet]
    public async Task<List<BookDto>> GetBooks([FromQuery] BookFilter filter)
    {
        var books = await _service.GetBooks(filter);
        return books;
    }

    [HttpGet("{id}")]
    public async Task<BookDto> GetBooksById(int id)
    {
        var books = await _service.GetBookById(id);
        return books;
    }    
}