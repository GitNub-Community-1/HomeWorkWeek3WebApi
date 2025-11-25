using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorController(IAuthorService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Author> AddAuthor(Author author)
    {
        var createdCategory = await _service.AddAuthor(author);
        return author;
    }
    
    [HttpPut]
    public async Task<Author> UpdateAuthor( Author author)
    { 
        var updateAuthor = await _service.UpdateAuthor(author);
        return updateAuthor;
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteAuthor(int id)
    {
        var result = await _service.DeleteAuthor(id);
        if(result>0)
        {
            return "Delete Succefully!";
        }

        return "Deleted not succefully!";
    }


    [HttpGet]
    public async Task<List<Author>> GetAuthors([FromQuery]AuthorsFilter filter)
    {
        var authors = await _service.GetAuthors(filter);
        return authors;
    }

    [HttpGet("{id}")]
    public async Task<Author> GetAuthorById(int id)
    {
        var author = await _service.GetAuthorsById(id);
        return author;
    }    
}