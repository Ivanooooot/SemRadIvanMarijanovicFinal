using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemRadIvanMarijanovic.Areas.Identity.Data;
using SemRadIvanMarijanovic.Models;

namespace SemRadIvanMarijanovic.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vehiclemodel> VehicleModels { get; set; }
    public DbSet<RentalStatus> RentalStatuses { get; set; }
    public DbSet<VehiclemodelCategory> VehicleCategories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        List<Category> mainCategories = new List<Category>()
        {
            new Category() { Id = 1, Title = "MDMR", Description = "Small cars" },
            new Category() { Id = 2, Title = "EDMD", Description = "Medium diesel cars" },
            new Category() { Id = 3, Title = "IDMD", Description = "Intermediate diesel cars" },
            new Category() { Id = 4, Title = "XGAR", Description = "Luxury SUV" },
            new Category() { Id = 5, Title = "FVMR", Description = "Passenger vans" },
        };

        builder.Entity<Category>().HasData(mainCategories);

        string adminRoleId = "66412151-dd0c-4b69-82c8-0f51551515";
        string adminRoleTitle = "Admin";
        string customerRoleId = "0e71d461-63e3-4aa5-be93-d708888888888";
        string customerRoleTitle = "Customer";

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = adminRoleId,
                Name = adminRoleTitle,
                NormalizedName = adminRoleTitle.ToUpper()
            },
            new IdentityRole()
            {
                Id = customerRoleId,
                Name = customerRoleTitle,
                NormalizedName = customerRoleTitle.ToUpper()
            }
        );

        string adminId = "6642155-dj2c-4819-82c8-0f4151555555551";
        string admin = "ivansem@gmail.com";
        string adminFirstName = "Ivan";
        string adminLastName = "Marijan";
        string adminPassword = "testSem123€";
        string adminAddress = "Put starog sela 20";

        var hasher = new PasswordHasher<ApplicationUser>();

        builder.Entity<ApplicationUser>().HasData(
                    new ApplicationUser()
                    {
                        Id = adminId,
                        UserName = admin,
                        NormalizedUserName = admin.ToUpper(),
                        Email = admin,
                        NormalizedEmail = admin.ToUpper(),
                        PasswordHash = hasher.HashPassword(null, adminPassword)
                    }
                );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                UserId = adminId,
                RoleId = adminRoleId
            }
        );

        List<Vehiclemodel> mainVehicleModels = new List<Vehiclemodel>()
        {
            new Vehiclemodel() { Id = 101, Title = "Toyota Aygo", Price = 20.12M, Image = "2023-06-22-10-32-34_1.jpg" },
            new Vehiclemodel() { Id = 102, Title = "Fiat 500", Price = 20.12M, Image = "2023-06-22-10-32-40_2.jpeg" },
            new Vehiclemodel() { Id = 103, Title = "VW Polo", Price = 30.32M, Image = "2023-06-22-10-32-46_3.jpg" },
            new Vehiclemodel() { Id = 104, Title = "Opel Corsa", Price = 30.32M, Image = "2023-06-22-10-32-52_4.jpeg" },
            new Vehiclemodel() { Id = 105, Title = "Renault Megane", Price = 39.4M, Image = "2023-06-22-10-32-58_5.jpg" },
            new Vehiclemodel() { Id = 106, Title = "VW Golf", Price = 39.4M, Image = "2023-06-22-10-33-03_6.jpg" },
            new Vehiclemodel() { Id = 107, Title = "Peugeot 3008 AUT.", Price = 84.19M, Image = "2023-06-22-10-33-10_7.jpg" },
            new Vehiclemodel() { Id = 108, Title = "Hyundai Tucson AUT.", Price = 84.19M, Image = "2023-06-22-10-33-16_8.jpg" },
            new Vehiclemodel() { Id = 109, Title = "Renault Trafic", Price = 74.12M, Image = "2023-06-22-10-33-21_9.jpg" },
            new Vehiclemodel() { Id = 110, Title = "Opel Zafira", Price = 74.12M, Image = "2023-06-22-10-33-26_10.jpg" },
        };

        builder.Entity<Vehiclemodel>().HasData(mainVehicleModels);

        List<VehiclemodelCategory> vehicleCategoriesData = new List<VehiclemodelCategory>()
        {
            new VehiclemodelCategory() { Id = 1, CategoryId = 1, VehiclemodelId = 101 },
            new VehiclemodelCategory() { Id = 2, CategoryId = 1, VehiclemodelId = 102 },
            new VehiclemodelCategory() { Id = 3, CategoryId = 2, VehiclemodelId = 103 },
            new VehiclemodelCategory() { Id = 4, CategoryId = 2, VehiclemodelId = 104 },
            new VehiclemodelCategory() { Id = 5, CategoryId = 3, VehiclemodelId = 105 },
            new VehiclemodelCategory() { Id = 6, CategoryId = 3, VehiclemodelId = 106 },
            new VehiclemodelCategory() { Id = 7, CategoryId = 4, VehiclemodelId = 107 },
            new VehiclemodelCategory() { Id = 8, CategoryId = 4, VehiclemodelId = 108 },
            new VehiclemodelCategory() { Id = 9, CategoryId = 5, VehiclemodelId = 109 },
            new VehiclemodelCategory() { Id = 10, CategoryId = 5, VehiclemodelId = 110 },
        };

        builder.Entity<VehiclemodelCategory>().HasData(vehicleCategoriesData);
    }

    public DbSet<SemRadIvanMarijanovic.Models.User>? User { get; set; }

}
