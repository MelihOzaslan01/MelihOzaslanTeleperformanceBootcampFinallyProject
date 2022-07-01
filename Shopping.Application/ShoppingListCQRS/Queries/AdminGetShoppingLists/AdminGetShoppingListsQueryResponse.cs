using Shopping.Domain.Dtos;

namespace Shopping.Application.ShoppingListCQRS.Queries.AdminGetShoppingLists;

public class AdminGetShoppingListsQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ShoppingListDto> ShoppingLists { get; set; }
}