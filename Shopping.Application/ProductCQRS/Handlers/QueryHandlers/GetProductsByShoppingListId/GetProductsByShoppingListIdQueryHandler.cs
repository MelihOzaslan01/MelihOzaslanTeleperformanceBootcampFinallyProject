using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.Product.Queries.GetProductsByShoppingListId;
using Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProductsByShoppingListId;

public class GetProductsByShoppingListIdQueryHandler:IRequestHandler<GetProductsByShoppingListIdQueryRequest,GetProductsByShoppingListIdQueryResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    
    public GetProductsByShoppingListIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsByShoppingListIdQueryResponse> Handle(GetProductsByShoppingListIdQueryRequest request, CancellationToken cancellationToken)
    {
        var getProductByShoppingListIdQueryResponse = new GetProductsByShoppingListIdQueryResponse();

        var products = await _productRepository.Get(x=>x.ShoppingListId==request.ShoppingListId);
        var productDtos = _mapper.Map<List<ProductDto>>(products);

        getProductByShoppingListIdQueryResponse.Products = productDtos;
        getProductByShoppingListIdQueryResponse.IsSuccess = true;
        
        return getProductByShoppingListIdQueryResponse;
        
    }
}