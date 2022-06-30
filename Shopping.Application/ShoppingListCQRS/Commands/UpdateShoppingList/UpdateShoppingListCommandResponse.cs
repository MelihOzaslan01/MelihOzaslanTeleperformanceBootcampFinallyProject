namespace Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;

public class UpdateShoppingListCommandResponse
{
    public bool IsSuccess { get; set; }
    public Domain.Entities.ShoppingList ShoppingList { get; set; }
}