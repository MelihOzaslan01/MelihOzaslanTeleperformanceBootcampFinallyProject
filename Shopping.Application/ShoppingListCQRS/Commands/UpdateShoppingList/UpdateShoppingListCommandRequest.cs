using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;

public class UpdateShoppingListCommandRequest:IRequest<UpdateShoppingListCommandResponse>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime CompleteDate { get; set; }
    public string Description { get; set; }
}