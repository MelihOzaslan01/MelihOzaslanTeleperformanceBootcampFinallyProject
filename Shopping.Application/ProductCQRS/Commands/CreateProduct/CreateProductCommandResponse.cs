namespace Shopping.Application.ProductCQRS.Commands.CreateProduct;

public class CreateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Domain.Entities.Product Product { get; set; }
}