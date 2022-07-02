using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CreateShoppingList;
using Xunit;

namespace Shopping.Tests.CommandHandlers.ShoppingList;

public class CreateShoppingListCommandHandlerTest
{
    [Fact]
    public async void CreateShoppingList_IsSuccess()
    {
        var createShoppingListCommandRequest = new Bogus.Faker<CreateShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Add(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();

        var command = new CreateShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(createShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ShoppingList);
    }
    [Fact]
    public async void CreateShoppingList_IsFailure()
    {
        var createShoppingListCommandRequest = new Bogus.Faker<CreateShoppingListCommandRequest>();
        
        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Add(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new CreateShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(createShoppingListCommandRequest.Generate(), CancellationToken.None);
        response.ShoppingList = null;
        
        Assert.False(response.IsSuccess);
        Assert.Null(response.ShoppingList);
    }
}