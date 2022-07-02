using AutoMapper;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProductById;
using Shopping.Application.ProductCQRS.Queries.GetProductById;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.Product;

public class GetProductByIdQueryHandlerTest
{
    [Fact]
    public async void GetProductByIdQueryHandler_IsSuccess()
    {
        var getProductsByIdQueryRequest = new Bogus.Faker<GetProductByIdQueryRequest>();
        var product = new Bogus.Faker<Domain.Entities.Product>().Generate();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(product);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ProductDto>(It.IsAny<Domain.Entities.Product>())).Returns(new Bogus.Faker<ProductDto>().Generate());
        
        var command = new GetProductByIdQueryHandler(mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsByIdQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Product);
    }
    [Fact]
    public async void GetProductByIdQueryHandler_IsFailure()
    {
        var getProductsByIdQueryRequest = new Bogus.Faker<GetProductByIdQueryRequest>();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(()=>null);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ProductDto>(It.IsAny<Domain.Entities.Product>())).Returns(()=>null);
        
        var command = new GetProductByIdQueryHandler(mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsByIdQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.Product);
    }
}