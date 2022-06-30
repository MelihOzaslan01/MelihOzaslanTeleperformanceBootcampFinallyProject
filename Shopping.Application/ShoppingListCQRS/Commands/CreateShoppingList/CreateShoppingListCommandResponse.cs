namespace Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;

public class CreateShoppingListCommandResponse
{
    public bool IsSuccess { get; set; }
    public Domain.Entities.ShoppingList ShoppingList { get; set; }
}