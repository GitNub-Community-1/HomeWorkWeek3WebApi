using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Services;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Response<CategoryDtos>> AddCategory(CreateCategoryDto category)
    {
        var result = await _service.AddCategory(category);
        return result;
    }
    
    [HttpPut]
    public async Task<Response<CategoryDtos>> UpdateCategory(CategoryDtos category)
    { 
        var result = await _service.UpdateCategory(category);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteCategory(int id)
    {
        var result = await _service.DeleteCategory(id);
        return result;
    }


    [HttpGet]
    public async Task<Response<PagedResponse<List<CategoryDtos>>>> GetCategories([FromQuery]CategoryFilter filter)
    {
        var result = await _service.GetCategories(filter);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<CategoryDtos>> GetCategoryById(int id)
    {
        var result = await _service.GetCategoryById(id);
        return result;
    }
}