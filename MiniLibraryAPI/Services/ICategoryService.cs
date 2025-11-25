using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Category> AddCategory(Category category);
    public Task<Category> UpdateCategory(Category category);
    public Task<int> DeleteCategory(int id);
    public  Task<List<Category>> GetCategories(CategoryFilter filter);
    public  Task<Category?> GetCategoryById(int id);
}