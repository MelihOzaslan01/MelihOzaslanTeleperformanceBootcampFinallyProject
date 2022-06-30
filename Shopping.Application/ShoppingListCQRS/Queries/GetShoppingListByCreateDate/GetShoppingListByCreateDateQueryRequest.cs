using MediatR;

namespace Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCreateDate;

public class GetShoppingListByCreateDateQueryRequest:IRequest<GetShoppingListByCreateDateQueryResponse>
{
    public DateTime CreateDate { get; set; }
}