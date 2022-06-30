using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Queries.GetProductById;
using Shopping.Application.ProductCQRS.Queries.GetProducts;
using Shopping.Domain.Dtos;

namespace Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProducts;

public class GetProductsQueryHandler:IRequestHandler<GetProductsQueryRequest,GetProductsQueryResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _distributedCache;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IDistributedCache distributedCache, IProductRepository productRepository, IMapper mapper)
    {
        _distributedCache = distributedCache;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
    {
        var getProductsQueryResponse = new GetProductsQueryResponse();
        
        byte[] cachedBytes = _distributedCache.GetAsync("products").Result;
        if (cachedBytes == null)
        {
            var products =  await _productRepository.GetAll();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            getProductsQueryResponse.Products = productDtos;
            getProductsQueryResponse.IsSuccess = true;

            string jsonText = JsonSerializer.Serialize(productDtos);
            await _distributedCache.SetAsync("products", Encoding.UTF8.GetBytes(jsonText), token: cancellationToken);
        }
        else
        {
            string jsonText = Encoding.UTF8.GetString(cachedBytes);
            var products = JsonSerializer.Deserialize<List<ProductDto>>(jsonText);
            getProductsQueryResponse.Products=products;
            getProductsQueryResponse.IsSuccess = true;
        }

        return getProductsQueryResponse;
    }
}