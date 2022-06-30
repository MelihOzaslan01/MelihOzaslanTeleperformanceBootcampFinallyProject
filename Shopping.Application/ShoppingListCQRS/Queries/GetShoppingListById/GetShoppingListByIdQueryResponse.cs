using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListById;

public class GetShoppingListByIdQueryResponse
{
    public bool IsSuccess { get; set; }
    public ShoppingListDto ShoppingList { get; set; }
}