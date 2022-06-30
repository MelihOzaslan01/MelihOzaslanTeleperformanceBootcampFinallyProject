using MediatR;
using Shopping.Application.Product.Queries.GetProductsByShoppingListId;

namespace Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;

public class GetProductsByShoppingListIdQueryRequest :IRequest<GetProductsByShoppingListIdQueryResponse>
{
    public int ShoppingListId { get; set; }
}