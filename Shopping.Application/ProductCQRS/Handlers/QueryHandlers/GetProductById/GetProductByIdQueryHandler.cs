using AutoMapper;
using MediatR;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Queries.GetProductById;
using Shopping.Application.ProductCQRS.Queries.GetProducts;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProductById;

public class GetProductByIdQueryHandler:IRequestHandler<GetProductByIdQueryRequest,GetProductByIdQueryResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var getProductByIdQueryResponse = new GetProductByIdQueryResponse();

        var product = await _productRepository.GetById(request.Id);
        var productDto = _mapper.Map<ProductDto>(product);
        
        getProductByIdQueryResponse.Product = productDto;
        getProductByIdQueryResponse.IsSuccess = product!=null;
        
        return getProductByIdQueryResponse;    
    }
}