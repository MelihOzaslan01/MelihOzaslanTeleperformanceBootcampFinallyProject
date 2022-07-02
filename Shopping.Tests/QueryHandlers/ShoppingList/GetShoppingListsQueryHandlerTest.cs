using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingLists;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.ShoppingList;

public class GetShoppingListsQueryHandlerTest
{
    [Fact]
    public async void GetShoppingLists_IsSuccess()
    {
        var getShoppingListsQueryRequest = new Bogus.Faker<GetShoppingListsQueryRequest>();
        var shoppingLists = new Bogus.Faker<List<Domain.Entities.ShoppingList>>().Generate();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.GetAll()).ReturnsAsync(shoppingLists);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(new Bogus.Faker<List<Domain.Entities.Product>>().Generate());
        
        var mockCache = new Mock<IDistributedCache>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ShoppingListDto>>(It.IsAny<List<Domain.Entities.ShoppingList>>())).Returns(new Bogus.Faker<List<ShoppingListDto>>().Generate());
        
        var command = new GetShoppingListsQueryHandler(mockShoppingListRepository.Object,mockCache.Object,mockProductRepository.Object, mapper.Object);
        
        var response = await command.Handle(getShoppingListsQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ShoppingLists);
    }
    [Fact]
    public async void GetShoppingLists_IsFailure()
    {
        var getShoppingListsQueryRequest = new Bogus.Faker<GetShoppingListsQueryRequest>();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.GetAll()).ReturnsAsync(()=>null);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(()=>null);
        
        var mockCache = new Mock<IDistributedCache>();
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<List<ShoppingListDto>>(It.IsAny<List<Domain.Entities.ShoppingList>>())).Returns(()=>null);
        
        var command = new GetShoppingListsQueryHandler(mockShoppingListRepository.Object,mockCache.Object,mockProductRepository.Object, mapper.Object);
        
        var response = await command.Handle(getShoppingListsQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.ShoppingLists);
    }
}