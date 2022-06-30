using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;

public class GetShoppingListsQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingLists { get; set; }
}