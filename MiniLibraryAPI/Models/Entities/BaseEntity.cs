using System.ComponentModel.DataAnnotations;

namespace MiniLibraryAPI.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }
}