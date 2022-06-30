using MediatR;

namespace Shopping.Application.ProductCQRS.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int ShoppingListId { get; init;  }
        public string Amount { get; init;  }
    }
}
