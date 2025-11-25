using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Book> AddBook(Book book)
    {
        var createdCategory = await _service.AddBook(book);
        return book;
    }
    
    [HttpPut]
    public async Task<Book> UpdateBook( Book book)
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
    public async Task<List<Book>> GetBooks([FromQuery] BookFilter filter)
    {
        var books = await _service.GetBooks(filter);
        return books;
    }

    [HttpGet("{id}")]
    public async Task<Book> GetBooksById(int id)
    {
        var books = await _service.GetBookById(id);
        return books;
    }    
}