using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCategory;

public class GetShoppingListByCategoryQueryRequest:IRequest<GetShoppingListByCategoryQueryResponse>
{
    public string Category { get; set; }
}