using System.ComponentModel.DataAnnotations;

namespace MiniLibraryAPI.Entities;

public class Order : BaseEntity
{
    public string? Name { get; set; }
    public DateTime OrderDate { get; set; }
    [MaxLength(12)]
    public string? Phone { get;set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}