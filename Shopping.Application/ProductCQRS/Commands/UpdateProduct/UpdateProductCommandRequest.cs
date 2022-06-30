using MediatR;

namespace Shopping.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int ShoppingListId { get; init;  }
        public string Amount { get; init;  }
    }
}
