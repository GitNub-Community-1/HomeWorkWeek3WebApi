using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<CategoryDtos> AddCategory(CreateCategoryDto category);
    public Task<Category> UpdateCategory(CategoryDtos category);
    public Task<int> DeleteCategory(int id);
    public  Task<List<CategoryDtos>> GetCategories(CategoryFilter filter);
    public  Task<CategoryDtos?> GetCategoryById(int id);
}