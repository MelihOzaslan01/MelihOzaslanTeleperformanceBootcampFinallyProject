using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.DeleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.DeleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.UpdateShoppingList;
using Xunit;

namespace Shopping.Tests.CommandHandlers.ShoppingList;

public class DeleteShoppingListCommandHandlerTest
{
    [Fact]
    public async void DeleteShoppingList_IsSuccess()
    {
        var deleteShoppingListCommandRequest = new Bogus.Faker<DeleteShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Delete(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(true);

        var mockCache = new Mock<IDistributedCache>();

        var command = new DeleteShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(deleteShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.True(response.IsSuccess);
    }
    [Fact]
    public async void DeleteShoppingList_IsFailure()
    {
        var deleteShoppingListCommandRequest = new Bogus.Faker<DeleteShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.Delete(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(false);

        var mockCache = new Mock<IDistributedCache>();

        var command = new DeleteShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(deleteShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.False(response.IsSuccess);
    }
}