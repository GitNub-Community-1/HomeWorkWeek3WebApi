using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Models.DTOs;

public class AuthorDto : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string FullName => $"{FirstName},{LastName}";
}