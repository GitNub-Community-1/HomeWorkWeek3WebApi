using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Services;

public interface IBookService
{
    Task<Response<BookDto>> AddBook(CreateBookDto book);
    Task<Response<BookDto>> UpdateBook(BookDto book);
    Task<Response<string>> DeleteBook(int id);
    Task<Response<PagedResponse<List<BookDto>>>> GetBooks(BookFilter filter);
    Task<Response<BookDto>> GetBookById(int id);
}