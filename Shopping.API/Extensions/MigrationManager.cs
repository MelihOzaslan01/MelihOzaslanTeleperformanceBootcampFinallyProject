using Microsoft.EntityFrameworkCore;
using Shopping.Infrastructure.Persistence;

namespace Shopping.API.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            try
            {
                var adminDbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
                var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (applicationDbContext.Database.ProviderName!="Microsoft.EntityFrameworkCore.InMemory")
                {
                    applicationDbContext.Database.Migrate();  
                    adminDbContext.Database.Migrate();
                }

                SeedData.SeedAsync(applicationDbContext,scope.ServiceProvider).Wait();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        return host;
    }
}