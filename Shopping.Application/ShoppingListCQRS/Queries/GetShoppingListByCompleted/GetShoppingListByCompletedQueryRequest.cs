using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleted;

public class GetShoppingListByCompletedQueryRequest:IRequest<GetShoppingListByCompletedQueryResponse>
{
    public bool IsSuccess { get; set; }
    public bool IsCompleted { get; set; }
}