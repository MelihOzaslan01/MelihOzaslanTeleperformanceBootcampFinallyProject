using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Shopping.Domain.Entities;

namespace Shopping.Infrastructure.Persistence;

public class SeedData
{
    public static async Task SeedAsync(ApplicationDbContext context,IServiceProvider serviceProvider)
    {
        if (!context.Users.Any())
        {
            var user = GetPreconfiguredUser();
            var userManager = serviceProvider.GetService<UserManager<User>>();
            await userManager.CreateAsync(user, user.Password);
            
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name,user.Email));
            claims.Add(new Claim(ClaimTypes.Role,"User"));
            claims.Add(new Claim(ClaimTypes.Role,"Admin"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            
            await userManager.AddClaimsAsync(user, claims);
        }
    }
    private static User GetPreconfiguredUser()
    {
        var user = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin@admin.com",
            Password = "Admin12345678+!",
            UserName = "admin@admin.com"
        };
        return user;
    }
}