using Shopping.Domain.Entities;

namespace Shopping.Application.Common.Interfaces.Repositories;

public interface IShoppingListRepository:IGenericRepository<ShoppingList>
{
    public Task<bool> CompleteShoppingList(int shoppingListId);
    public Task<bool> AdminAdd(ShoppingList entity);
    public Task<IEnumerable<ShoppingList>> AdminGetAll();

}