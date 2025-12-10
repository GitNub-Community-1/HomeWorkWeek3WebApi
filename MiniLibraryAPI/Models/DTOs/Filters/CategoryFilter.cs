namespace MiniLibraryAPI.DTOs;

public class CategoryFilter
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}