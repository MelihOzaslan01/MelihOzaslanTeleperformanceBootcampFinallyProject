using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;

namespace Shopping.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Domain.Entities.Product> Products { get; }
        DbSet<ShoppingList> ShoppingLists { get; }

    }
}
