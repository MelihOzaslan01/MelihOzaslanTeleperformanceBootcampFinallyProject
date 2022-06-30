using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByTitle;

public class GetShoppingListByTitleQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
}