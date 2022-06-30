using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByTitle;

public class GetShoppingListByTitleQueryRequest:IRequest<GetShoppingListByTitleQueryResponse>
{
    public string Title { get; set; }
}