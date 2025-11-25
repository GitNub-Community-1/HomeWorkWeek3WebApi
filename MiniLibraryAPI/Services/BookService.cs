using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class BookService(ApplicationDbContext context) : IBookService
{
    public async Task<Book> AddBook(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateBook(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task<int> DeleteBook(int id)
    {
        var book = await context.Books.FindAsync(id);
        context.Books.Remove(book);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<Book>> GetBooks(BookFilter filter)
    {
        var query = context.Books
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
          
        if (filter.AuthorId.HasValue)
        {
            query = query.Where(x => x.AuthorId == filter.AuthorId.Value);
        }
          
        if (filter.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == filter.CategoryId.Value);
        }
        return await query.ToListAsync();
    }

    public async Task<Book> GetBookById(int id)
    {
        return await context.Books.FirstOrDefaultAsync(b => b.Id == id);
    }
}