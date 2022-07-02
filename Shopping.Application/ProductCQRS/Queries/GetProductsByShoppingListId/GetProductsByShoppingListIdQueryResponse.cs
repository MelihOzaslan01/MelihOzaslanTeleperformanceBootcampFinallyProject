using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;

public class GetProductsByShoppingListIdQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ProductDto> Products { get; set; }
}