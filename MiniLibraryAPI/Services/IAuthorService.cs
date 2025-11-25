using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface IAuthorService
{
    Task<Author> AddAuthor(Author author);
    Task<Author> UpdateAuthor(Author author);
    Task<int> DeleteAuthor(int id);
    Task<List<Author>> GetAuthors(AuthorsFilter filter);
    Task<Author> GetAuthorsById(int id);
    
}