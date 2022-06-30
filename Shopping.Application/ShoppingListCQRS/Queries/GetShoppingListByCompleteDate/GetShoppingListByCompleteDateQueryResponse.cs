using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleteDate;

public class GetShoppingListByCompleteDateQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingList { get; set; }
}