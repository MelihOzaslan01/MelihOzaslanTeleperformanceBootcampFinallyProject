using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Handlers.CommandHandlers.CreateProduct;
using Xunit;

namespace Shopping.Tests.CommandHandlers.Product;

public class CreateProductCommandHandlerTest
{
    [Fact]
    public async void CreateProduct_IsSuccess()
    {
        var createProductCommandRequest = new Bogus.Faker<CreateProductCommandRequest>();

        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Add(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();

        var command = new CreateProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(createProductCommandRequest.Generate(), CancellationToken.None);
        
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Product);
    }
    [Fact]
    public async void CreateProduct_IsFailure()
    {
        var createProductCommandRequest = new Bogus.Faker<CreateProductCommandRequest>();
        Domain.Entities.Product product = null;
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Add(It.IsAny<Domain.Entities.Product>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new CreateProductCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(createProductCommandRequest.Generate(), CancellationToken.None);
        response.Product = product;

        Assert.False(response.IsSuccess);
        Assert.Null(response.Product);
    }
}