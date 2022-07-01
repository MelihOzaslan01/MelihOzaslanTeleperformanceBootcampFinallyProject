using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;

namespace Shopping.Application.Common.Interfaces;

public interface IAdminDbContext
{
    DbSet<ShoppingList> ShoppingLists { get; }
}