using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{ 
    public async Task<Category> AddCategory(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategory(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<int> DeleteCategory(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null)
            return 0;

        context.Categories.Remove(category);
        return await context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetCategories(CategoryFilter filter)
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
        
        return await query.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }
}