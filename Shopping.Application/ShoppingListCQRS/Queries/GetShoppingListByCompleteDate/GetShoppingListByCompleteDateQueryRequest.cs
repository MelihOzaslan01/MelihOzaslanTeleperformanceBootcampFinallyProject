using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleteDate;

public class GetShoppingListByCompleteDateQueryRequest:IRequest<GetShoppingListByCompleteDateQueryResponse>
{
    public bool IsSuccess { get; set; }
    public DateTime CompleteDate { get; set; }
}