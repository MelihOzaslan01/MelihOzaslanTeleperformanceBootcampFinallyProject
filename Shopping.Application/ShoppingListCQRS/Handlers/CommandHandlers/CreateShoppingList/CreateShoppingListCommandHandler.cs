using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;

namespace Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CreateShoppingList;

public class CreateShoppingListCommandHandler:IRequestHandler<CreateShoppingListCommandRequest,CreateShoppingListCommandResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IDistributedCache _distributedCache;

    public CreateShoppingListCommandHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
    }

    public async Task<CreateShoppingListCommandResponse> Handle(CreateShoppingListCommandRequest request, CancellationToken cancellationToken)
    {
        var shoppingList = new Domain.Entities.ShoppingList
        {
            Title = request.Title,
            Category = request.Category,
            CompleteDate = null,
            CreateDate = DateTime.Now,
            Description = request.Description,
            IsCompleted = false
        };
        var createShoppingListCommandResponse = new CreateShoppingListCommandResponse();
        var result = await _shoppingListRepository.Add(shoppingList);

        createShoppingListCommandResponse.IsSuccess = result;
        createShoppingListCommandResponse.ShoppingList = shoppingList;

        if (createShoppingListCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);
        }

        return createShoppingListCommandResponse;
    }
}