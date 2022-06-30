using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByDescription;

public class GetShoppingListByDescriptionQueryRequest:IRequest<GetShoppingListByDescriptionQueryResponse>
{
    public string Description { get; set; }
}