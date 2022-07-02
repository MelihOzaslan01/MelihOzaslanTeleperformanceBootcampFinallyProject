using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListById;
using Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingLists;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListById;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.ShoppingList;

public class GetShoppingListsByIdQueryHandlerTest
{
    [Fact]
    public async void GetShoppingListById_IsSuccess()
    {
        var getShoppingListsByIdQueryRequest = new Bogus.Faker<GetShoppingListByIdQueryRequest>();
        var shoppingList = new Bogus.Faker<Domain.Entities.ShoppingList>().Generate();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(shoppingList);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>> >())).ReturnsAsync(new Bogus.Faker<List<Domain.Entities.Product>>().Generate());
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ShoppingListDto>(It.IsAny<Domain.Entities.ShoppingList>())).Returns(new Bogus.Faker<ShoppingListDto>().Generate());
        
        var command = new GetShoppingListByIdQueryHandler(mapper.Object,mockProductRepository.Object,mockShoppingListRepository.Object);
        
        var response = await command.Handle(getShoppingListsByIdQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ShoppingList);
    }
    [Fact]
    public async void GetShoppingLists_IsFailure()
    {
        var getShoppingListsByIdQueryRequest = new Bogus.Faker<GetShoppingListByIdQueryRequest>();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(()=>null);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>> >())).ReturnsAsync(()=>null);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ShoppingListDto>(It.IsAny<Domain.Entities.ShoppingList>())).Returns(()=>null);
        
        var command = new GetShoppingListByIdQueryHandler(mapper.Object,mockProductRepository.Object,mockShoppingListRepository.Object);
        
        var response = await command.Handle(getShoppingListsByIdQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.ShoppingList);
    }
}