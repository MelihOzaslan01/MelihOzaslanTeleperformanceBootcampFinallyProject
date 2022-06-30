using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListById;

public class GetShoppingListByIdQueryRequest:IRequest<GetShoppingListByIdQueryResponse>
{
    public int Id { get; set; }
}