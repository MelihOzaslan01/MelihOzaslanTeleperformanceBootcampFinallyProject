using Microsoft.EntityFrameworkCore;
using Shopping.Application.Common.Interfaces;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence
{
    public class AdminDbContext : DbContext,IAdminDbContext
    {
        public AdminDbContext()
        {
            
        }
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("DefaultShoppingAdminLocal");
            }
        }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

    }
}