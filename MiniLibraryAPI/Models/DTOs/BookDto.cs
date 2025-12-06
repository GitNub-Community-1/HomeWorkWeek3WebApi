using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Models.DTOs;

public class BookDto : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public long AuthorId { get; set; }
    public long CategoryId { get; set; }
}