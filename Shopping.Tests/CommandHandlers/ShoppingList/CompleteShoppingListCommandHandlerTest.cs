using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CompleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CompleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CreateShoppingList;
using Xunit;

namespace Shopping.Tests.CommandHandlers.ShoppingList;

public class CompleteShoppingListCommandHandlerTest
{
    [Fact]
    public async void CompleteShoppingList_IsSuccess()
    {
        var completeShoppingListCommandRequest = new Bogus.Faker<CompleteShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.CompleteShoppingList(It.IsAny<int>())).ReturnsAsync(true);
        mockRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(new Bogus.Faker<Domain.Entities.ShoppingList>().Generate());
        mockRepository.Setup(c => c.AdminAdd(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(true);
        var mockCache = new Mock<IDistributedCache>();

        var command = new CompleteShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(completeShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.True(response.IsSuccess);
    }
    [Fact]
    public async void CompleteShoppingList_IsFailure()
    {
        var completeShoppingListCommandRequest = new Bogus.Faker<CompleteShoppingListCommandRequest>();

        var mockRepository = new Mock<IShoppingListRepository>();
        mockRepository.Setup(c => c.CompleteShoppingList(It.IsAny<int>())).ReturnsAsync(false);
        mockRepository.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(new Bogus.Faker<Domain.Entities.ShoppingList>().Generate());
        mockRepository.Setup(c => c.AdminAdd(It.IsAny<Domain.Entities.ShoppingList>())).ReturnsAsync(false);
        var mockCache = new Mock<IDistributedCache>();

        var command = new CompleteShoppingListCommandHandler(mockRepository.Object, mockCache.Object);
        
        var response = await command.Handle(completeShoppingListCommandRequest.Generate(), CancellationToken.None);
        
        Assert.False(response.IsSuccess);
    }
}