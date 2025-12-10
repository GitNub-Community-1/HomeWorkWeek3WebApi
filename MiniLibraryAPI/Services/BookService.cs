using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;
using System.Net;

namespace MiniLibraryAPI.Services;

public class BookService(ApplicationDbContext context, IMapper mapper) : IBookService
{
    public async Task<Response<BookDto>> AddBook(CreateBookDto book_) 
    {
        try
        {
            var book = mapper.Map<Book>(book_);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var result = mapper.Map<BookDto>(book);
            return new Response<BookDto>(HttpStatusCode.Created, "Book created successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<BookDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<BookDto>> UpdateBook(BookDto book)
    {
        try
        {
            var check = await context.Books.FindAsync(book.Id);
            if (check == null)
                return new Response<BookDto>(HttpStatusCode.NotFound, "Book not found");
            
            check.Name = book.Name;
            check.Description = book.Description;
            check.AuthorId = book.AuthorId;
            check.CategoryId = book.CategoryId;
            context.Books.Update(check);
            await context.SaveChangesAsync();
            var result = mapper.Map<BookDto>(check);
            return new Response<BookDto>(HttpStatusCode.OK, "Book updated successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<BookDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<string>> DeleteBook(int id)
    {
        try
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return new Response<string>(HttpStatusCode.NotFound, "Book not found");
            
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Book deleted successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<PagedResponse<List<BookDto>>>> GetBooks(BookFilter filter)
    {
        try
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
                query = query.Where(x => x.Description != null && x.Description.Contains(filter.Description));
            }
              
            if (filter.AuthorId.HasValue)
            {
                query = query.Where(x => x.AuthorId == filter.AuthorId.Value);
            }
              
            if (filter.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == filter.CategoryId.Value);
            }
            
            var totalRecords = await query.CountAsync();
            
            var page = filter.Page > 0 ? filter.Page : 1;
            var size = filter.Size > 0 ? filter.Size : 20;

            var books = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            
            var result = mapper.Map<List<BookDto>>(books);

            var pagedResponse = new PagedResponse<List<BookDto>>
            {
                Data = result,
                Page = page,
                Size = size,
                TotalRecords = totalRecords
            };

            return new Response<PagedResponse<List<BookDto>>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Books retrieved successfully!",
                Data = pagedResponse
            };
        }
        catch (Exception ex)
        {
            return new Response<PagedResponse<List<BookDto>>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<BookDto>> GetBookById(int id)
    {
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return new Response<BookDto>(HttpStatusCode.NotFound, "Book not found");
            
            var result = mapper.Map<BookDto>(book);
            return new Response<BookDto>(HttpStatusCode.OK, "Book retrieved successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<BookDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }
}