using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.UpdateShoppingList;
using Xunit;

namespace Shopping.Tests.CommandHandlers.ShoppingList;

public class UpdateShoppingListCommandHandlerTest
{
    [Fact]
    public async void UpdateShoppingList_IsSuccess()
    {
        var updateShoppingListCommandRequest = new Bogus.Faker<UpdateShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Update(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();

        var command = new UpdateShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(updateShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.ShoppingList);
    }
    [Fact]
    public async void UpdateShoppingList_IsFailure()
    {
        var updateShoppingListCommandRequest = new Bogus.Faker<UpdateShoppingListCommandRequest>();
        
        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Add(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new UpdateShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(updateShoppingListCommandRequest.Generate(), CancellationToken.None);
        response.ShoppingList = null;
        
        Assert.False(response.IsSuccess);
        Assert.Null(response.ShoppingList);
    }
}