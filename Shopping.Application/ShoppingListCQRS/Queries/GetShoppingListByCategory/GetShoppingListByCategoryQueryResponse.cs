using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCategory;

public class GetShoppingListByCategoryQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
}