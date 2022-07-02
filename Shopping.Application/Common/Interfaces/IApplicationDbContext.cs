using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;

namespace Shopping.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Product> Products { get; }
        DbSet<ShoppingList> ShoppingLists { get; }

    }
}
