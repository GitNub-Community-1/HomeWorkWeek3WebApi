using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Services;

public interface ICategoryService
{
    Task<Response<CategoryDtos>> AddCategory(CreateCategoryDto category);
    public Task<Response<CategoryDtos>> UpdateCategory(CategoryDtos category);
    public Task<Response<string>> DeleteCategory(int id);
    public  Task<Response<PagedResponse<List<CategoryDtos>>>> GetCategories(CategoryFilter filter);
    public  Task<Response<CategoryDtos?>> GetCategoryById(int id);
}