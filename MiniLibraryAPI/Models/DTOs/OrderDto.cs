using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Models.DTOs;

public class OrderDto : BaseEntity
{
    public string? Name { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Phone { get;set; }
    public int BookId { get; set; }
}