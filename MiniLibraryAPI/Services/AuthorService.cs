using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public class AuthorService(ApplicationDbContext context, IMapper mapper) : IAuthorService
{
    public async Task<AuthorDto> AddAuthor(CreateAuthorDto author_)
    {
        var author = mapper.Map<Author>(author_);
        context.Authors.Add(author);
        await context.SaveChangesAsync();
        return mapper.Map<AuthorDto>(author);
    }

    public async Task<Author> UpdateAuthor(AuthorDto author_)
    {
        var check = await context.Authors.FindAsync(author_.Id);
        check.FirstName = author_.FirstName;
        check.LastName = author_.LastName;
        context.Authors.Update(check);
        await context.SaveChangesAsync();
        return mapper.Map<Author>(author_);    
    }

    public async Task<int> DeleteAuthor(int id)
    {
        var author = await context.Authors.FindAsync(id);
        context.Authors.Remove(author);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<AuthorDto>> GetAuthors(AuthorsFilter filter)
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
        var author = await query.ToListAsync();
        var translate = mapper.Map<List<AuthorDto>>(author);
        return translate;
    }

    public async Task<AuthorDto?> GetAuthorsById(int id)
    {
        var author = await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        var translate = mapper.Map<AuthorDto>(author);
        return translate;
    }
}