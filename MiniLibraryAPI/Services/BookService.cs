using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public class BookService(ApplicationDbContext context, IMapper mapper) : IBookService
{
    public async Task<BookDto> AddBook(CreateBookDto book_)
    {
        var book = mapper.Map<Book> (book_);
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return mapper.Map<BookDto>(book);
    }

    public async Task<Book> UpdateBook(BookDto book)
    {
        var check = await context.Books.FindAsync(book.Id);
        check.Name = book.Name;
        check.Description = book.Description;
        check.AuthorId = book.AuthorId;
        check.CategoryId = book.CategoryId;
        context.Books.Update(check);
        await context.SaveChangesAsync();
        return  mapper.Map<Book>(book);
    }

    public async Task<int> DeleteBook(int id)
    {
        var book = await context.Books.FindAsync(id);
        context.Books.Remove(book);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<BookDto>> GetBooks(BookFilter filter)
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
        var books = await query.ToListAsync();
        var translate = mapper.Map<List<BookDto>>(books);
        return translate;
    }

    public async Task<BookDto> GetBookById(int id)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        var translate = mapper.Map<BookDto>(book);
        return translate;
    }
}