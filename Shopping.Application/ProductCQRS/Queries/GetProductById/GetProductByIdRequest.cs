using MediatR;
using Shopping.Application.ProductCQRS.Queries.GetProducts;

namespace Shopping.Application.ProductCQRS.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
