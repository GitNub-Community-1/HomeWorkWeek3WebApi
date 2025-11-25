using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface IBookService
{
    Task<Book> AddBook(Book book);
    Task<Book> UpdateBook(Book book);
    Task<int> DeleteBook(int id);
    Task<List<Book>> GetBooks(BookFilter filter);
    Task<Book> GetBookById(int id);
}