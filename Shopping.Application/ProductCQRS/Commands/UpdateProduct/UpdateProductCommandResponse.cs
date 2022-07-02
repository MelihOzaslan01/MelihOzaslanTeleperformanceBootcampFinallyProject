using Shopping.Domain.Entities;

namespace Shopping.Application.ProductCQRS.Commands.UpdateProduct;

public class UpdateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Product Product { get; set; }
}