using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;

public class CreateShoppingListCommandRequest:IRequest<CreateShoppingListCommandResponse>
{
    public string Title { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime CompleteDate { get; set; }
    public string Description { get; set; }
}