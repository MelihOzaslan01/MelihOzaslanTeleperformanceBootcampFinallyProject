using MediatR;

namespace Shopping.Application.ProductCQRS.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        
        public string Name { get; init; }
        public int ShoppingListId { get; init;  }
        public string Amount { get; init;  }
    }
}
