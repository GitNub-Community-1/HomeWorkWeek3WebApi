using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class AuthorService(ApplicationDbContext context) : IAuthorService
{
    public async Task<Author> AddAuthor(Author author)
    {
        context.Authors.Add(author);
        await context.SaveChangesAsync();
        return author;
    }

    public async Task<Author> UpdateAuthor(Author author)
    {
        context.Authors.Update(author);
        await context.SaveChangesAsync();
        return author;
    }

    public async Task<int> DeleteAuthor(int id)
    {
        var author = await context.Authors.FindAsync(id);
        context.Authors.Remove(author);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<Author>> GetAuthors(AuthorsFilter filter)
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

        return await query.ToListAsync();
    }

    public async Task<Author?> GetAuthorsById(int id)
    {
        return await context.Authors.FirstOrDefaultAsync(a => a.Id == id);
    }
}