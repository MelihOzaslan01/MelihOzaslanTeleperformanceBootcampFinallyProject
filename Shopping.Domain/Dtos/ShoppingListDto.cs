using Shopping.Domain.Entities;

namespace Shopping.Domain.Dtos;

public class ShoppingListDto
{
    public string Title { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; } 
    public DateTime CreateDate { get; set; } 
    public DateTime? CompleteDate { get; set; }
    public string Description { get; set; }
    public List<ProductDto> Products { get; set; }
}