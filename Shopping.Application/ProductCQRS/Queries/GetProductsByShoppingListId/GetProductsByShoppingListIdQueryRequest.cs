using MediatR;

namespace Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;

public class GetProductsByShoppingListIdQueryRequest :IRequest<GetProductsByShoppingListIdQueryResponse>
{
    public int ShoppingListId { get; set; }
}