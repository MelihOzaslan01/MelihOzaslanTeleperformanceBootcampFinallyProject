using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping.Application.Common.Interfaces;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext,IApplicationDbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
