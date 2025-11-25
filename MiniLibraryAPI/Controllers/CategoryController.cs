using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Category> AddCategory(Category category)
    {
        var createdCategory = await _service.AddCategory(category);
        return createdCategory;
    }
    
    [HttpPut]
    public async Task<Category> UpdateCategory( Category category)
    { 
        var updatedCategory = await _service.UpdateCategory(category);
        return updatedCategory;
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteCategory(int id)
    {
        var result = await _service.DeleteCategory(id);
        if(result>0)
        {
            return "Delete Succefully!";
        }

        return "Deleted not succefully!";
    }


    [HttpGet]
    public async Task<List<Category>> GetCategories([FromQuery]CategoryFilter filter)
    {
        var categories = await _service.GetCategories(filter);
        return categories;
    }

    [HttpGet("{id}")]
    public async Task<Category> GetCategoryById(int id)
    {
        var category = await _service.GetCategoryById(id);
        return category;
    }
}