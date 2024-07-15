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
            context.Groups.AddRange( group1, group2, group3);
            await context.SaveChangesAsync();

            Entity entity1 = new Entity() {Name = "Tata Power", GroupId = group1.Id};
            Entity entity2 = new Entity() { Name = "Tata Chemicals", GroupId = group1.Id };
            Entity entity3 = new Entity() { Name = "Tata Motors", GroupId = group1.Id };
            Entity entity4 = new Entity() { Name = "Adani Power", GroupId = group2.Id };
            Entity entity5 = new Entity() { Name = "Adani Ent", GroupId = group2.Id };
            Entity entity6 = new Entity() { Name = "Adani Wilmar", GroupId = group2.Id };
            Entity entity7 = new Entity() { Name = "Reliance Infra", GroupId = group3.Id };
            Entity entity8 = new Entity() { Name = "Reliance Petro", GroupId = group3.Id };
            Entity entity9 = new Entity() { Name = "Reliance Power", GroupId = group3.Id };
            context.Entities.AddRange(entity1, entity2, entity3, entity4, entity5,entity6, entity7, entity8, entity9);
            await context.SaveChangesAsync();

            Country country1 = new Country() { CountryName = "India" };
            Country country2 = new Country() { CountryName = "USA" };
            Country country3 = new Country() { CountryName = "Canada" };
            Country country4 = new Country() { CountryName = "Australia" };
            context.Countries.AddRange(country1, country2, country3, country4);
            await context.SaveChangesAsync();

            GroupCountry groupCountry1 = new GroupCountry() { GroupId =group1.Id, CountryId = country1.CountryId }; //Tata-India
            GroupCountry groupCountry2 = new GroupCountry() { GroupId = group1.Id, CountryId = country2.CountryId }; //Tata-USA
            GroupCountry groupCountry3 = new GroupCountry() { GroupId = group2.Id, CountryId = country1.CountryId };//Adani-India
            GroupCountry groupCountry4 = new GroupCountry() { GroupId = group2.Id, CountryId = country3.CountryId };//Adani-Canada
            GroupCountry groupCountry5 = new GroupCountry() { GroupId = group3.Id, CountryId = country1.CountryId };//Reliance-India
            GroupCountry groupCountry6 = new GroupCountry() { GroupId = group3.Id, CountryId = country4.CountryId };//Reliance-Aus
            context.GroupCountries.AddRange(groupCountry1, groupCountry2, groupCountry3, groupCountry4, groupCountry5, groupCountry6);
            
            EntityCountry entityCountry1 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity1.Id };//TataPower India
            EntityCountry entityCountry2 = new EntityCountry() { CountryId = country2.CountryId, EntityId = entity2.Id };//Tata Chem- USA
            EntityCountry entityCountry3 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity3.Id };//Tata Motors-India
            EntityCountry entityCountry4 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity4.Id };//Adani Power-India
            EntityCountry entityCountry5 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity5.Id };//Adani Ent -India
            EntityCountry entityCountry6 = new EntityCountry() { CountryId = country3.CountryId, EntityId = entity6.Id };//Adani wilman - Canada
            EntityCountry entityCountry7 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity7.Id };//Rel infra-India
            EntityCountry entityCountry8 = new EntityCountry() { CountryId = country1.CountryId, EntityId = entity8.Id };//Rel petro -India
            EntityCountry entityCountry9 = new EntityCountry() { CountryId = country4.CountryId, EntityId = entity9.Id };//Rel Power - Aust
            context.EntityCountries.AddRange(entityCountry1, entityCountry2, entityCountry3, entityCountry4, entityCountry5, entityCountry6, entityCountry7
                entityCountry8, entityCountry9);
            await context.SaveChangesAsync();

            State state1 = new State() { StateName = "MH", CountryId = country1.CountryId };
            State state2 = new State() { StateName = "MP", CountryId = country1.CountryId };
            State state3 = new State() { StateName = "KA", CountryId = country1.CountryId };
            State state4 = new State() { StateName = "Texas", CountryId = country2.CountryId };
            State state5 = new State() { StateName = "Victoria", CountryId = country4.CountryId };
            State state6 = new State() { StateName = "Manitoba", CountryId = country3.CountryId };
            context.States.AddRange(state1, state2, state3, state4, state5, state6);
            await context.SaveChangesAsync();

            EntityStateMapping entityStateMapping1 = new EntityStateMapping() { EntityId = entity1.Id, StateId = state1.StateId };
            EntityStateMapping entityStateMapping2 = new EntityStateMapping() { EntityId = entity2.Id, StateId = state4.StateId };
            EntityStateMapping entityStateMapping3 = new EntityStateMapping() { EntityId = entity3.Id, StateId = state3.StateId };
            EntityStateMapping entityStateMapping4 = new EntityStateMapping() { EntityId = entity4.Id, StateId = state1.StateId };
            EntityStateMapping entityStateMapping5 = new EntityStateMapping() { EntityId = entity5.Id, StateId = state3.StateId };
            EntityStateMapping entityStateMapping6 = new EntityStateMapping() { EntityId = entity6.Id, StateId = state5.StateId };
            EntityStateMapping entityStateMapping7 = new EntityStateMapping() { EntityId = entity7.Id, StateId = state1.StateId };
            EntityStateMapping entityStateMapping8 = new EntityStateMapping() { EntityId = entity8.Id, StateId = state1.StateId };
            context.EntityStates.AddRange(entityStateMapping1, entityStateMapping2, entityStateMapping3, entityStateMapping4,
                                            entityStateMapping5, entityStateMapping6, entityStateMapping7, entityStateMapping8);
            var result = await context.SaveChangesAsync();

            City city1 = new City() { CityName = "Pune", StateId = state1.StateId };
            City city2 = new City() { CityName = "Mumbai", StateId = state1.StateId };
            City city3 = new City() { CityName = "Bangalore", StateId = state3.StateId };
            City city4 = new City() { CityName = "Mysore", StateId = state3.StateId };
            City city5 = new City() { CityName = "Bhopal", StateId = state2.StateId };
            City city6 = new City() { CityName = "India", StateId = state2.StateId };
            City city7 = new City() { CityName = "Utah", StateId = state4.StateId };
            City city8 = new City() { CityName = "Vermont", StateId = state4.StateId };
            City city9 = new City() { CityName = "Melbourne", StateId = state5.StateId };
            City city10 = new City() { CityName = "Brandon", StateId = state6.StateId };
            context.Cities.AddRange(city1, city2, city3, city4, city5, city6, city7, city8, city9, city10);
            await context.SaveChangesAsync();

            Module module1 = new Module() { Name = "DirectTax" };
            Module module2 = new Module() { Name = "In-DirectTax" };
            Module module3 = new Module() { Name = "Others" };
            context.Modules.AddRange(module1, module2, module3);
            await context.SaveChangesAsync();

            GroupModule groupModule1 = new GroupModule() { GroupId = group1.Id, ModuleId = module1.Id};
            GroupModule groupModule2 = new GroupModule() { GroupId = group1.Id, ModuleId = module2.Id };
            GroupModule groupModule3 = new GroupModule() { GroupId = group2.Id, ModuleId = module1.Id };
            GroupModule groupModule4 = new GroupModule() { GroupId = group2.Id, ModuleId = module2.Id };
            GroupModule groupModule5 = new GroupModule() { GroupId = group2.Id, ModuleId = module3.Id };
            GroupModule groupModule6 = new GroupModule() { GroupId = group3.Id, ModuleId = module1.Id };
            context.GroupModules.AddRange(groupModule1, groupModule2, groupModule3, groupModule4, groupModule5, groupModule6);
            await context.SaveChangesAsync();


            Submodule submodule1 = new Submodule() { Name = "TNA", ModuleId = module1.Id };
            Submodule submodule2 = new Submodule() { Name = "1336", ModuleId = module1.Id };
            Submodule submodule3 = new Submodule() { Name = "Refund Tracker", ModuleId = module1.Id };
            Submodule submodule4 = new Submodule() { Name = "Litigation Tracker", ModuleId = module2.Id };
            Submodule submodule5 = new Submodule() { Name = "Compliance", ModuleId = module2.Id };
            Submodule submodule6 = new Submodule() { Name = "Compliance", ModuleId = module1.Id };
            context.Submodules.AddRange(submodule1, submodule2, submodule3, submodule4, submodule5, submodule6);
            await context.SaveChangesAsync();

            



        }
    }


}
