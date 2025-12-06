using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Models.DTOs;

public class CategoryDtos : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
}