using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context, IMapper mapper) : ICategoryService
{ 
    public async Task<CategoryDtos> AddCategory(CreateCategoryDto categoryDto)
    {
        var category = mapper.Map<Category>(categoryDto);
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return mapper.Map<CategoryDtos>(category);
    }

    public async Task<Category> UpdateCategory(CategoryDtos category)
    {
        var check = await context.Categories.FindAsync(category.Id);
        check.Name = category.Name;
        check.Description = category.Description;
        context.Categories.Update(check);
        await context.SaveChangesAsync();
        return mapper.Map<Category>(category);
    }

    public async Task<int> DeleteCategory(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
            return 0;

        context.Categories.Remove(category);
        return await context.SaveChangesAsync();
    }

    public async Task<List<CategoryDtos>> GetCategories(CategoryFilter filter)
    {
        var query = context.Categories
            .AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.Contains(filter.Description));
        }
        var category =  await query.ToListAsync();
        var translate = mapper.Map<List<CategoryDtos>>(category);
        return translate;
    }

    public async Task<CategoryDtos?> GetCategoryById(int id)
    {
        var category = await context.Books.FirstOrDefaultAsync(a => a.Id == id);
        var translate = mapper.Map<CategoryDtos>(category);
        return translate;
    }
}