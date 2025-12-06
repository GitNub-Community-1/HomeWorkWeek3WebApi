namespace MiniLibraryAPI.Models.DTOs;

public class CreateBookDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public long AuthorId { get; set; }
    public long CategoryId { get; set; }
}