using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.UpdateProduct;
using Shopping.Domain.Entities;


namespace Shopping.Application.ProductCQRS.Handlers.CommandHandlers.UpdateProduct;

public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IDistributedCache _distributedCache;


    public UpdateProductCommandHandler(IProductRepository productRepository, IDistributedCache distributedCache)
    {
        _productRepository = productRepository;
        _distributedCache = distributedCache;
    }
    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
    {
        
        var updateProductCommandResponse = new UpdateProductCommandResponse();
      
        var productToUpdate = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Amount = request.Amount,
            ShoppingListId = request.ShoppingListId
        };
        var result = await _productRepository.Update(productToUpdate);

        updateProductCommandResponse.IsSuccess = result;
        updateProductCommandResponse.Product = productToUpdate;
        
        if (updateProductCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("products", cancellationToken);
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);
        }
        
        return updateProductCommandResponse;
    }
}