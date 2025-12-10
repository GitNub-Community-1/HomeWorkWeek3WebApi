namespace MiniLibraryAPI.DTOs;

public class BookFilter
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long? AuthorId { get; set; }
    public long? CategoryId { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}