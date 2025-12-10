namespace MiniLibraryAPI.DTOs;

public class OrderFilter
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Phone { get;set; }
    public DateTime? OrderDate { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}