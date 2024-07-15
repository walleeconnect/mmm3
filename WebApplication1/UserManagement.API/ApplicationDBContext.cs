namespace UserManagement.API
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Microsoft.AspNetCore.Identity;
    using System.Security;
    using System.Data;


    //[Flags]
    //public enum Permissions
    //{
    //    None = 0,
    //    ManageDirectTax = 1 << 0,
    //    ManageInDirectTax = 1 << 1,
    //    ManageCompliance = 1 << 2,
    //    DirectTaxReadOnly = 1 << 3,
    //    DirectTaxAddOnly = 1 << 4,
    //    DirectTaxModifyOnly = 1 << 5,
    //    DirectTaxUploadOnly = 1 << 6,
    //    DirectTaxDeleteOnly = 1 << 7,
    //    InDirectTaxReadOnly = 1 << 8,
    //    InDirectTaxAddOnly = 1 << 9,
    //    InDirectTaxModifyOnly = 1 << 10,
    //    InDirectTaxUploadOnly = 1 << 11,
    //    InDirectTaxDeleteOnly = 1 << 12,
    //    ComplianceTaxReadOnly = 1 << 13,
    //    ComplianceTaxAddOnly = 1 << 14,
    //    ComplianceTaxModifyOnly = 1 << 15,
    //    ComplianceTaxUploadOnly = 1 << 16,
    //    ComplianceDeleteOnly = 1 << 17
    //}
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Entity> Entities { get; set; }
    }
    public class EntityOwner
    {
        public int EntityOwnerId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
    }
    public class UserGroup
    {
        public int UserGroupId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }

    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class State
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }

    public class Submodule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityCountry
    {
        public int EntityCountryId { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    public class EntityStateMapping
    {
        public int EntityStateId { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }

    public class EntityCity
    {
        public int EntityCityId { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
    public class GroupCountry
    {
        public int GroupCountryId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }

    //public class UserPermission
    //{
    //    public int Id { get; set; }
    //    public string UserId { get; set; }
    //    public ApplicationUser User { get; set; }

    //    public int GroupId { get; set; }
    //    public Group Group { get; set; }

    //    public int EntityId { get; set; }
    //    public Entity Entity { get; set; }

    //    public int ModuleId { get; set; }
    //    public Module Module { get; set; }

    //    public int SubmoduleId { get; set; }
    //    public Submodule Submodule { get; set; }

    //    public int PermissionId { get; set; }
    //    public Permission Permission { get; set; }
    //}
    public class UserPermission
    {
        public int UserPermissionId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int? SubmoduleId { get; set; }
        public Submodule Submodule { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public DbSet<GroupModule> GroupModules { get; set; }
        public DbSet<GroupSubmodule> GroupSubmodules { get; set; }
        public DbSet<EntityModule> EntityModules { get; set; }
        public DbSet<EntitySubmodule> EntitySubmodules { get; set; }
    }
    public class EntitySubmodule
    {
        public int EntitySubmoduleId { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int SubmoduleId { get; set; }
        public Submodule Submodule { get; set; }
    }

    public class EntityModule
    {
        public int EntityModuleId { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }

    public class GroupSubmodule
    {
        public int GroupSubmoduleId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int SubmoduleId { get; set; }
        public Submodule Submodule { get; set; }
    }

    public class GroupModule
    {
        public int GroupModuleId { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
    public class ApplicationUser : IdentityUser
    {
        public int TenantId { get; set; }
        public string Role { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string role):base(role) { }
        //public string Description { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Submodule> Submodules { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<EntityOwner> EntityOwners { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<GroupCountry> GroupCountries { get; set; }
        public DbSet<EntityCountry> EntityCountries { get; set; }
        public DbSet<EntityStateMapping> EntityStateMapping { get; set; }
        public DbSet<GroupModule> GroupModules { get; set; }
        public DbSet<GroupSubmodule> GroupSubmodules { get; set; }
        public DbSet<EntityModule> EntityModules { get; set; }
        public DbSet<EntitySubmodule> EntitySubmodules { get; set; }
        public DbSet<EntityCity> EntityCities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserGroupId });

            builder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany()
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany()
                .HasForeignKey(ug => ug.GroupId);

            builder.Entity<EntityOwner>()
                .HasKey(eo => new { eo.EntityOwnerId });

            builder.Entity<EntityOwner>()
                .HasOne(eo => eo.User)
                .WithMany()
                .HasForeignKey(eo => eo.UserId);

            builder.Entity<EntityOwner>()
                .HasOne(eo => eo.Entity)
                .WithMany()
                .HasForeignKey(eo => eo.EntityId);
            builder.Entity<EntityStateMapping>()
               .HasKey(up => new { up.EntityStateId });

            builder.Entity<UserPermission>()
                .HasKey(up => new { up.UserPermissionId });

            builder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Module)
                .WithMany()
                .HasForeignKey(up => up.ModuleId);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Submodule)
                .WithMany()
                .HasForeignKey(up => up.SubmoduleId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Country)
                .WithMany()
                .HasForeignKey(up => up.CountryId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPermission>()
                .HasOne(up => up.State)
                .WithMany()
                .HasForeignKey(up => up.StateId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPermission>()
                .HasOne(up => up.City)
                .WithMany()
                .HasForeignKey(up => up.CityId);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany()
                .HasForeignKey(up => up.PermissionId);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Group)
                .WithMany()
                .HasForeignKey(up => up.GroupId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Entity)
                .WithMany()
                .HasForeignKey(up => up.EntityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Group-Entity relationship
            builder.Entity<Entity>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Entities)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
