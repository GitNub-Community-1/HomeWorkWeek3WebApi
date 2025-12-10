using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Services;

public interface IAuthorService
{
    Task<Response<AuthorDto>> AddAuthor(CreateAuthorDto author);
    Task<Response<AuthorDto>> UpdateAuthor(AuthorDto author);
    Task<Response<string>> DeleteAuthor(int id);
    Task<Response<PagedResponse<List<AuthorDto>>>> GetAuthors(AuthorsFilter filter);
    Task<Response<AuthorDto>> GetAuthorsById(int id);
    
}