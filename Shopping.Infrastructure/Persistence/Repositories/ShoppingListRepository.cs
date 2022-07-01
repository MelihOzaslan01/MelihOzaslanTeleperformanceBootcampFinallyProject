using Microsoft.EntityFrameworkCore;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence.Repositories
{
    public class ShoppingListRepository : GenericRepository<ShoppingList>, IShoppingListRepository
    {
        protected readonly AdminDbContext _adminDbContext;
        public ShoppingListRepository(ApplicationDbContext applicationDbContext, AdminDbContext adminDbContext) : base(applicationDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        public async Task<bool> CompleteShoppingList(int shoppingListId)
        {
            var shoppingList = await _dbSet.FirstOrDefaultAsync(x => x.Id == shoppingListId);
            if (shoppingList==null)
            {
                return false;
            }
            shoppingList.IsCompleted = true;
            shoppingList.CompleteDate=DateTime.Now;
            _dbSet.Update(shoppingList);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result>0;
        }

        public async Task<bool> AdminAdd(ShoppingList entity)
        {
            await _adminDbContext.ShoppingLists.AddAsync(entity);
            var result = await _adminDbContext.SaveChangesAsync();
            return result>0;
        }

        public async Task<IEnumerable<ShoppingList>> AdminGetAll()
        {
            return await _adminDbContext.ShoppingLists.ToListAsync();        
        }
    }
}