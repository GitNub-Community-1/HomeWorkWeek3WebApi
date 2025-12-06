using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public interface IBookService
{
    Task<BookDto> AddBook(CreateBookDto book);
    Task<Book> UpdateBook(BookDto book);
    Task<int> DeleteBook(int id);
    Task<List<BookDto>> GetBooks(BookFilter filter);
    Task<BookDto> GetBookById(int id);
}