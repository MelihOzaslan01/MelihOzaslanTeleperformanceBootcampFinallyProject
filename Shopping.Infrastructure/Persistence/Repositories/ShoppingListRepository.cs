using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence.Repositories
{
    public class ShoppingListRepository : GenericRepository<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}