using System.Linq.Expressions;
using AutoMapper;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Handlers.QueryHandlers.GetShoppingListByTitle;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByTitle;
using Shopping.Domain.Dtos;
using Xunit;

namespace Shopping.Tests.QueryHandlers.ShoppingList;

public class GetShoppingListByTitleQueryHandlerTest
{
    [Fact]
    public async void GetShoppingListByTitle_IsSuccess()
    {
        var getShoppingListByTitleQueryRequest = new Bogus.Faker<GetShoppingListByTitleQueryRequest>();
        var shoppingLists = new Bogus.Faker<List<Domain.Entities.ShoppingList>>().Generate();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Domain.Entities.ShoppingList,bool>>>())).ReturnsAsync(shoppingLists);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(new Bogus.Faker<List<Domain.Entities.Product>>().Generate());
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ShoppingListDto>(It.IsAny<Domain.Entities.ShoppingList>())).Returns(new Bogus.Faker<ShoppingListDto>().Generate());
        
        var command = new GetShoppingListByTitleQueryHandler(mockShoppingListRepository.Object,mockProductRepository.Object,mapper.Object);
        
        var response = await command.Handle(getShoppingListByTitleQueryRequest.Generate(), CancellationToken.None);

        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ShoppingList);
    }
    [Fact]
    public async void GetShoppingListByTitle_IsFailure()
    {
        var getShoppingListByTitleQueryRequest = new Bogus.Faker<GetShoppingListByTitleQueryRequest>();
        
        var mockShoppingListRepository = new Mock<IShoppingListRepository>();
        mockShoppingListRepository.Setup(c => c.Get(It.IsAny<Expression<Func<Domain.Entities.ShoppingList,bool>>>())).ReturnsAsync(()=>null);
        var mockProductRepository = new Mock<IProductRepository>();
        mockProductRepository.Setup(c=>c.Get(It.IsAny<Expression<Func<Domain.Entities.Product,bool>>>())).ReturnsAsync(()=>null);
        
        var mapper = new Mock<IMapper>();
        mapper.Setup(x => x.Map<ShoppingListDto>(It.IsAny<Domain.Entities.ShoppingList>())).Returns(()=>null);
        
        var command = new GetShoppingListByTitleQueryHandler(mockShoppingListRepository.Object,mockProductRepository.Object,mapper.Object);
        
        var response = await command.Handle(getShoppingListByTitleQueryRequest.Generate(), CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Null(response.ShoppingList);
    }
}