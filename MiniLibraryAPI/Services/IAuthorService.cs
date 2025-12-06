using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public interface IAuthorService
{
    Task<AuthorDto> AddAuthor(CreateAuthorDto author);
    Task<Author> UpdateAuthor(AuthorDto author);
    Task<int> DeleteAuthor(int id);
    Task<List<AuthorDto>> GetAuthors(AuthorsFilter filter);
    Task<AuthorDto> GetAuthorsById(int id);
    
}