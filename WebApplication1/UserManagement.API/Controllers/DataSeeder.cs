using Microsoft.AspNetCore.Identity;

namespace UserManagement.API.Controllers
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("GroupOwner"));
                await roleManager.CreateAsync(new IdentityRole("EntityOwner"));
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!context.Permissions.Any())
            {
                context.Permissions.AddRange(
                    new Permission { Name = "Add" },
                    new Permission { Name = "Update" },
                    new Permission { Name = "Upload" },
                    new Permission { Name = "View" }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Groups.Any())
            {
                var group = new Group { Name = "Group A" };
                context.Groups.Add(group);
                await context.SaveChangesAsync();

                var entity = new Entity { Name = "Entity 1", GroupId = group.Id };
                context.Entities.Add(entity);
                await context.SaveChangesAsync();
            }

            if (!context.Modules.Any())
            {
                var module = new Module { Name = "Direct Tax" };
                context.Modules.Add(module);
                await context.SaveChangesAsync();

                var submodule = new Submodule { Name = "Submodule1", ModuleId = module.Id };
                context.Submodules.Add(submodule);
                await context.SaveChangesAsync();
            }
        }
    }


}
