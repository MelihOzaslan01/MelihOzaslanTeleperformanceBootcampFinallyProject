using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Queries.GetProductById;

public class GetProductsQueryResponse
{
    public bool IsSuccess { get; set; }
    public List<ProductDto> Products { get; set; }
}