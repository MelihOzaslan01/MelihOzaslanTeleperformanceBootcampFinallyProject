using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.DeleteProduct;

namespace Shopping.Application.ProductCQRS.Handlers.CommandHandlers.DeleteProduct;

public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommandRequest,DeleteProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _distributedCache;


    public DeleteProductCommandHandler(IProductRepository productRepository, IDistributedCache distributedCache)
    {
        _productRepository = productRepository;
        _distributedCache = distributedCache;
    }
    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
    {
        
        var deleteProductCommandResponse = new DeleteProductCommandResponse();
        var product = await _productRepository.GetById(request.Id);

        var result = await _productRepository.Delete(product);

        deleteProductCommandResponse.IsSuccess = result;
        
        if (deleteProductCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("products", cancellationToken);
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);

        }
        
        return deleteProductCommandResponse;
    }
}