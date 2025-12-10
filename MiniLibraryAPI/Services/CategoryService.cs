using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;
using System.Net;

namespace MiniLibraryAPI.Services;

public class CategoryService(ApplicationDbContext context, IMapper mapper) : ICategoryService
{ 
    public async Task<Response<CategoryDtos>> AddCategory(CreateCategoryDto categoryDto)
    {
        try
        {
            var category = mapper.Map<Category>(categoryDto);
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            var result = mapper.Map<CategoryDtos>(category);
            return new Response<CategoryDtos>(HttpStatusCode.Created, "Category created successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<CategoryDtos>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<CategoryDtos>> UpdateCategory(CategoryDtos category)
    {
        try
        {
            var check = await context.Categories.FindAsync(category.Id);
            if (check == null)
                return new Response<CategoryDtos>(HttpStatusCode.NotFound, "Category not found");
            
            check.Name = category.Name;
            check.Description = category.Description;
            context.Categories.Update(check);
            await context.SaveChangesAsync();
            var result = mapper.Map<CategoryDtos>(check);
            return new Response<CategoryDtos>(HttpStatusCode.OK, "Category updated successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<CategoryDtos>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<string>> DeleteCategory(int id)
    {
        try
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
                return new Response<string>(HttpStatusCode.NotFound, "Category not found");

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Category deleted successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<PagedResponse<List<CategoryDtos>>>> GetCategories(CategoryFilter filter)
    {
        try
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
                query = query.Where(x => x.Description != null && x.Description.Contains(filter.Description));
            }
            
            var totalRecords = await query.CountAsync();
            
            var page = filter.Page > 0 ? filter.Page : 1;
            var size = filter.Size > 0 ? filter.Size : 20;

            var categories = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            
            var result = mapper.Map<List<CategoryDtos>>(categories);

            var pagedResponse = new PagedResponse<List<CategoryDtos>>
            {
                Data = result,
                Page = page,
                Size = size,
                TotalRecords = totalRecords
            };

            return new Response<PagedResponse<List<CategoryDtos>>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Categories retrieved successfully!",
                Data = pagedResponse
            };
        }
        catch (Exception ex)
        {
            return new Response<PagedResponse<List<CategoryDtos>>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<CategoryDtos?>> GetCategoryById(int id)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(a => a.Id == id);
            if (category == null)
                return new Response<CategoryDtos?>(HttpStatusCode.NotFound, "Category not found");
            
            var result = mapper.Map<CategoryDtos>(category);
            return new Response<CategoryDtos?>(HttpStatusCode.OK, "Category retrieved successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<CategoryDtos?>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }
}