using System.Linq.Expressions;
using AutoMapper;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ProductCQRS.Handlers.QueryHandlers.GetProductsByShoppingListId;
using Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.Product;

public class GetProductsByShoppingListIdQueryHandlerTest
{
    [Fact]
    public async void GetProductByIdQueryHandler_IsSuccess()
    {
        var getProductsByShoppingListIdQueryRequest = new Bogus.Faker<GetProductsByShoppingListIdQueryRequest>();
        var products = new Bogus.Faker<List<Domain.Entities.Product>>().Generate();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(products);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Domain.Entities.Product>>())).Returns(new Bogus.Faker<List<ProductDto>>().Generate());
        
        var command = new GetProductsByShoppingListIdQueryHandler(mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsByShoppingListIdQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Products);
    }
    [Fact]
    public async void GetProductByIdQueryHandler_IsFailure()
    {
        var getProductsByShoppingListIdQueryRequest = new Bogus.Faker<GetProductsByShoppingListIdQueryRequest>();
        
        var mockRepository = new Mock<IProductRepository>();
        mockRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(()=>null);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ProductDto>>(It.IsAny<List<Domain.Entities.Product>>())).Returns(()=>null);
        
        var command = new GetProductsByShoppingListIdQueryHandler(mockRepository.Object, mapper.Object);
        
        var response = await command.Handle(getProductsByShoppingListIdQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.Products);
    }
}