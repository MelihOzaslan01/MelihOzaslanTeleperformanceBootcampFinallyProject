using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Commands.DeleteShoppingList;

public class DeleteShoppingListCommandRequest:IRequest<DeleteShoppingListCommandResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; } 
    public DateTime CreateDate { get; set; } 
    public DateTime CompleteDate { get; set; }
    public string Description { get; set; }
}