using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Commands.DeleteProduct;
using Shopping.Application.ProductCQRS.Commands.UpdateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.CreateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.DeleteProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.UpdateProduct;
using Xunit;

namespace Shopping.Tests.CommandHandlers.Product;

public class DeleteProductCommandHandlerTest
{
    [Fact]
    public async void DeleteProduct_IsSuccess()
    {
        var deleteProductCommandRequest = new Bogus.Faker<DeleteProductCommandRequest>();

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Delete(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();
        
        var command = new DeleteProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(deleteProductCommandRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
    }
    [Fact]
    public async void UpdateProduct_IsFailure()
    {
        var deleteProductCommandRequest = new Bogus.Faker<DeleteProductCommandRequest>();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Delete(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new DeleteProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(deleteProductCommandRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
    }
}