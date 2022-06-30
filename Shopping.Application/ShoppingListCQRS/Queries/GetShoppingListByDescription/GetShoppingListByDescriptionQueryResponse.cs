using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByDescription;

public class GetShoppingListByDescriptionQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
}