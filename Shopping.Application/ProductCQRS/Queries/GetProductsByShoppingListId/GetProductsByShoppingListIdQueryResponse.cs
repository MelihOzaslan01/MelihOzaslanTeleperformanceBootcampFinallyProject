using Shopping.Domain.Dtos;
using Shopping.Domain.Entities;

namespace Shopping.Application.Product.Queries.GetProductsByShoppingListId;

public class GetProductsByShoppingListIdQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ProductDto> Products { get; set; }
}