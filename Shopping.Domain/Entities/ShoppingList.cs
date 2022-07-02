using System.ComponentModel.DataAnnotations;

namespace Shopping.Domain.Entities;

public class ShoppingList:IBaseEntity
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Category { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime? CompleteDate { get; set; }
    [Required]
    public string Description { get; set; }
}