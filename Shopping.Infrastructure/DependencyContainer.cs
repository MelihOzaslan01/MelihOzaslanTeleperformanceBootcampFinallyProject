using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Application.Common.Interfaces.Repositories;
using Shopping.Infrastructure.Persistence;
using Shopping.Infrastructure.Persistence.Repositories;

namespace Shopping.Infrastructure
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration,string env="Development")
        {
            if (env=="Test")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("appTestDb"));
                services.AddDbContext<AdminDbContext>(options =>
                    options.UseInMemoryDatabase("adminAppTestDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultShopping"))
                );
                services.AddDbContext<AdminDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultShoppingAdmin"))
                );
            
                services.AddTransient<IProductRepository, ProductRepository>();
                services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
                services.AddTransient<IUserRepository, UserRepository>();
            }

            return services;
        }
    }
}
