using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleted;

public class GetShoppingListByCompletedQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
}