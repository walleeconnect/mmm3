using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using UserManagement.API;


namespace TestProject1
{
   

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add the in-memory database context
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.EnableSensitiveDataLogging();
                });

                // Build the service provider
                var serviceProvider = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    // Ensure the database is created
                    db.Database.EnsureCreated();

                    // Seed the database with initial data
                    await SeedDatabase(db, userManager, roleManager);
                }
            });
        }

        private async Task SeedDatabase(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed initial data for tests
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("GroupOwner"));
                await roleManager.CreateAsync(new IdentityRole("EntityOwner"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser { UserName = "testuser", Email = "testuser@example.com", Role = "User" };
               await userManager.CreateAsync(user, "Test@1234");
                await userManager.AddToRoleAsync(user, "User");
            }

            Group group1 = new Group() { Name = "Tata" };
            Group group2 = new Group() { Name = "Adani" };
            Group group3 = new Group() { Name = "Reliance" };
            context.Add<Group>(group1);
            await context.SaveChangesAsync();
            context.Add<Group>(group2);
            await context.SaveChangesAsync();
            context.Add<Group>(group3);
            await context.SaveChangesAsync();
        }
    }


}
