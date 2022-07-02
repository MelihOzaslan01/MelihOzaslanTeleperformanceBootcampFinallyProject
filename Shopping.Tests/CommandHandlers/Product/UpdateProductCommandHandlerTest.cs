using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Commands.UpdateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.CreateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.UpdateProduct;
using Xunit;

namespace Shopping.Tests.CommandHandlers.Product;

public class UpdateProductCommandHandlerTest
{
    [Fact]
    public async void UpdateProduct_IsSuccess()
    {
        var updateProductCommandRequest = new Bogus.Faker<UpdateProductCommandRequest>();
        var product = new Bogus.Faker<Domain.Entities.Product>().Generate();


        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Update(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();
        
        var command = new UpdateProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(updateProductCommandRequest.Generate(), CancellationToken.None);
        response.Product = product;

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Product);
    }
    [Fact]
    public async void UpdateProduct_IsFailure()
    {
        var updateProductCommandRequest = new Bogus.Faker<UpdateProductCommandRequest>();
        Domain.Entities.Product product = null;


        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Update(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new UpdateProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(updateProductCommandRequest.Generate(), CancellationToken.None);
        response.Product = product;

        Assert.False(response.IsSuccess);
        Assert.Null(response.Product);
    }
}