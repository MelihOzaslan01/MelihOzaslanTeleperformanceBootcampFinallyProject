using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;

namespace Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.UpdateShoppingList;

public class UpdateShoppingListCommandHandler:IRequestHandler<UpdateShoppingListCommandRequest,UpdateShoppingListCommandResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IDistributedCache _distributedCache;

    public UpdateShoppingListCommandHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
    }

    public async Task<UpdateShoppingListCommandResponse> Handle(UpdateShoppingListCommandRequest request, CancellationToken cancellationToken)
    {
        var shoppingList = new Domain.Entities.ShoppingList
        {
            Id = request.Id,
            Title = request.Title,
            Category = request.Category,
            CompleteDate = null,
            CreateDate = DateTime.Now,
            Description = request.Description,
            IsCompleted = false
        };
        var updateShoppingListCommandResponse = new UpdateShoppingListCommandResponse();
        var result = await _shoppingListRepository.Update(shoppingList);

        updateShoppingListCommandResponse.IsSuccess = result;
        updateShoppingListCommandResponse.ShoppingList = shoppingList;

        if (updateShoppingListCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);
        }

        return updateShoppingListCommandResponse;
    }
}