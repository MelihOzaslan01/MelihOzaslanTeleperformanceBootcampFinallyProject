using Shopping.Domain.Entities;


namespace Shopping.Application.ProductCQRS.Commands.CreateProduct;

public class CreateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Product Product { get; set; }
}