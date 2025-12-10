using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Services;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorController(IAuthorService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Response<AuthorDto>> AddAuthor(CreateAuthorDto author)
    {
        var result = await _service.AddAuthor(author);
        return result;
    }
    
    [HttpPut]
    public async Task<Response<AuthorDto>> UpdateAuthor(AuthorDto author)
    { 
        var result = await _service.UpdateAuthor(author);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteAuthor(int id)
    {
        var result = await _service.DeleteAuthor(id);
        return result;
    }


    [HttpGet]
    public async Task<Response<PagedResponse<List<AuthorDto>>>> GetAuthors([FromQuery]AuthorsFilter filter)
    {
        var result = await _service.GetAuthors(filter);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<AuthorDto>> GetAuthorById(int id)
    {
        var result = await _service.GetAuthorsById(id);
        return result;
    }    
}