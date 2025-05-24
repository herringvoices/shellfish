using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shelfish.Api.Models;

namespace Shelfish.Api.Data;

public static class SeedData
{
    public static async Task InitialiseAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var users = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        await ctx.Database.MigrateAsync();

        const string adminEmail = "admin@shelfish.dev";
        const string adminPwd = "Admin123!";

        var admin = await users.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new User
            {
                UserName = "admin",
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
            };
            await users.CreateAsync(admin, adminPwd);
        }

        if (!ctx.Libraries.Any())
        {
            ctx.Libraries.Add(
                new Library
                {
                    Name = "Admin's Public Library",
                    IsPublic = true,
                    Description = "Demo library created by seed data",
                    LibraryCode = Guid.NewGuid().ToString("N").Substring(0, 8),
                    UserId = admin.Id,
                }
            );
            await ctx.SaveChangesAsync();
        }
    }
}
