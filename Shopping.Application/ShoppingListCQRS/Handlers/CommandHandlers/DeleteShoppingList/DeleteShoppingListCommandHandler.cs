using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Application.ShoppingListCQRS.Commands.DeleteShoppingList;

namespace Shopping.Application.ShoppingListCQRS.Handlers.CommandHandlers.DeleteShoppingList;

public class DeleteShoppingListCommandHandler:IRequestHandler<DeleteShoppingListCommandRequest,DeleteShoppingListCommandResponse>
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IDistributedCache _distributedCache;

    public DeleteShoppingListCommandHandler(IShoppingListRepository shoppingListRepository, IDistributedCache distributedCache)
    {
        _shoppingListRepository = shoppingListRepository;
        _distributedCache = distributedCache;
    }

    public async Task<DeleteShoppingListCommandResponse> Handle(DeleteShoppingListCommandRequest request, CancellationToken cancellationToken)
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
        var deleteShoppingListCommandResponse = new DeleteShoppingListCommandResponse();
        var result = await _shoppingListRepository.Delete(shoppingList);

        deleteShoppingListCommandResponse.IsSuccess = result;

        if (deleteShoppingListCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("shoppingLists", cancellationToken);
        }

        return deleteShoppingListCommandResponse;
    }
}