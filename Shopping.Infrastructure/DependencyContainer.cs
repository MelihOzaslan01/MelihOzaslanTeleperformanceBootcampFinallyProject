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
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"))
            );

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
