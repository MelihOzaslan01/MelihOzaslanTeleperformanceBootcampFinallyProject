using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.CompleteShoppingList;
using Shopping.Domain.Entities;

namespace Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.CompleteShoppingList;

public class CompleteShoppingListCommandHandler:IRequestHandler<CompleteShoppingListCommandRequest,CompleteShoppingListCommandResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IDistributedCache _distributedCache;

    public CompleteShoppingListCommandHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
    }

    public async Task<CompleteShoppingListCommandResponse> Handle(CompleteShoppingListCommandRequest request, CancellationToken cancellationToken)
    {
   
        var completeShoppingListCommandResponse = new CompleteShoppingListCommandResponse();
        var completeResult = await _shoppingListRepository.CompleteShoppingList(request.Id);
        
        completeShoppingListCommandResponse.IsSuccess = completeResult;

        var getShoppingList = await _shoppingListRepository.GetById(request.Id);
        var shoppingListToAdd = new ShoppingList
        {
            Title = getShoppingList.Title,
            Category = getShoppingList.Category,
            CompleteDate = getShoppingList.CompleteDate,
            CreateDate = getShoppingList.CreateDate,
            Description = getShoppingList.Description,
            IsCompleted = getShoppingList.IsCompleted
        };
        var addResult = await _shoppingListRepository.AdminAdd(shoppingListToAdd);
        
        completeShoppingListCommandResponse.IsSuccess = addResult;
        
        if (completeShoppingListCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("adminShoppingLists", cancellationToken);
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);
        }

        return completeShoppingListCommandResponse;
    }
}