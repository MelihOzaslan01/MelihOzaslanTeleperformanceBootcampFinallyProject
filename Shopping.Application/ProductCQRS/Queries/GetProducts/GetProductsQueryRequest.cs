using MediatR;
using Shopping.Application.ProductCQRS.Queries.GetProductById;

namespace Shopping.Application.ProductCQRS.Queries.GetProducts;

public record GetProductsQueryRequest : IRequest<GetProductsQueryResponse>
{
}