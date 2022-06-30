namespace Shopping.Application.ProductCQRS.Commands.UpdateProduct;

public class UpdateProductCommandResponse
{
    public bool IsSuccess { get; set; }
    public Domain.Entities.Product Product { get; set; }
}