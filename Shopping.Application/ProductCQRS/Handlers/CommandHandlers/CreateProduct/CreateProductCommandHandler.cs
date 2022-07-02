using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Domain.Entities;


namespace Shopping.Application.ProductCQRS.Handlers.CommandHandlers.CreateProduct;


    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDistributedCache _distributedCache;


        public CreateProductCommandHandler(IProductRepository productRepository, IDistributedCache distributedCache)
        {
            _productRepository = productRepository;
            _distributedCache = distributedCache;
        }
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                ShoppingListId = request.ShoppingListId,
                Amount = request.Amount
            };
            var createProductCommandResponse = new CreateProductCommandResponse();
            var result = await _productRepository.Add(product);

            createProductCommandResponse.IsSuccess = result;
            createProductCommandResponse.Product = product;

            if (createProductCommandResponse.IsSuccess)
            {
                await _distributedCache.RemoveAsync("products", cancellationToken);
                await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);

            }
            
            return createProductCommandResponse;
        }
    }
