using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;
using System.Net;

namespace MiniLibraryAPI.Services;

public class AuthorService(ApplicationDbContext context, IMapper mapper) : IAuthorService
{
    public async Task<Response<AuthorDto>> AddAuthor(CreateAuthorDto author_)
    {
        try
        {
            var author = mapper.Map<Author>(author_);
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            var result = mapper.Map<AuthorDto>(author);
            return new Response<AuthorDto>(HttpStatusCode.Created, "Author created successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<AuthorDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<AuthorDto>> UpdateAuthor(AuthorDto author_)
    {
        try
        {
            var check = await context.Authors.FindAsync(author_.Id);
            if (check == null)
                return new Response<AuthorDto>(HttpStatusCode.NotFound, "Author not found");
            
            check.FirstName = author_.FirstName;
            check.LastName = author_.LastName;
            context.Authors.Update(check);
            await context.SaveChangesAsync();
            var result = mapper.Map<AuthorDto>(check);
            return new Response<AuthorDto>(HttpStatusCode.OK, "Author updated successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<AuthorDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<string>> DeleteAuthor(int id)
    {
        try
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
                return new Response<string>(HttpStatusCode.NotFound, "Author not found");
            
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Author deleted successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<PagedResponse<List<AuthorDto>>>> GetAuthors(AuthorsFilter filter)
    {
        try
        {
            var query = context.Authors
                .AsQueryable();
            
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.Id == filter.Id.Value);
            }
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(x => x.LastName != null && x.LastName.Contains(filter.LastName));
            }
            
            var totalRecords = await query.CountAsync();
            
            var page = filter.Page > 0 ? filter.Page : 1;
            var size = filter.Size > 0 ? filter.Size : 20;

            var authors = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            
            var result = mapper.Map<List<AuthorDto>>(authors);

            var pagedResponse = new PagedResponse<List<AuthorDto>>
            {
                Data = result,
                Page = page,
                Size = size,
                TotalRecords = totalRecords
            };

            return new Response<PagedResponse<List<AuthorDto>>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Authors retrieved successfully!",
                Data = pagedResponse
            };
        }
        catch (Exception ex)
        {
            return new Response<PagedResponse<List<AuthorDto>>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<AuthorDto>> GetAuthorsById(int id)
    {
        try
        {
            var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return new Response<AuthorDto>(HttpStatusCode.NotFound, "Author not found");
            
            var result = mapper.Map<AuthorDto>(author);
            return new Response<AuthorDto>(HttpStatusCode.OK, "Author retrieved successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<AuthorDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }
}