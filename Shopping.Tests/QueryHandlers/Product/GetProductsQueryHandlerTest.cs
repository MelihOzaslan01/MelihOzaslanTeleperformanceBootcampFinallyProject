using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProducts;
using Shopping.Application.ProductCQRS.Queries.GetProducts;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.Product;

public class GetProductsQueryHandlerTest
{
    [Fact]
    public async void GetProducts_IsSuccess()
    {
        var getProductsQueryRequest = new Bogus.Faker<GetProductsQueryRequest>();
        var products = new Bogus.Faker<List<Domain.Entities.Product>>().Generate();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.GetAll()).ReturnsAsync(products);
        
        var mockCache = new Mock<IDistributedCache>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Domain.Entities.Product>>())).Returns(new Bogus.Faker<List<ProductDto>>().Generate());
        
        var command = new GetProductsQueryHandler(mockCache.Object,mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Products);
    }
    [Fact]
    public async void GetProducts_IsFailure()
    {
        var getProductsQueryRequest = new Bogus.Faker<GetProductsQueryRequest>();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.GetAll()).ReturnsAsync(()=>null);
        
        var mockCache = new Mock<IDistributedCache>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Domain.Entities.Product>>())).Returns(()=>null);
        
        var command = new GetProductsQueryHandler(mockCache.Object,mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.Products);
    }
}