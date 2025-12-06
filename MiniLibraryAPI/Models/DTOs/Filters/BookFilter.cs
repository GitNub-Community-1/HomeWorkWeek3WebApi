namespace MiniLibraryAPI.DTOs;

public class BookFilter
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? AuthorId { get; set; }
    public long? CategoryId { get; set; }
}