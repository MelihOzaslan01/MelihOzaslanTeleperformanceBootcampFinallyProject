using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Commands.CompleteShoppingList;

public class CompleteShoppingListCommandRequest:IRequest<CompleteShoppingListCommandResponse>
{
    public int Id { get; set; }
}