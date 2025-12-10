namespace MiniLibraryAPI.DTOs;

public class AuthorsFilter 
{
    public long? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 20;
}