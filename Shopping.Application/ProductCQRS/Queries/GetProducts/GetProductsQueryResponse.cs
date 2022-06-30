using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Queries.GetProducts;

public class GetProductByIdQueryResponse
{
    public bool IsSuccess { get; set; }
    public ProductDto Product { get; set; }
}