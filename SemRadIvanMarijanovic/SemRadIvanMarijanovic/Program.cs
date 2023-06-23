using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SemRadIvanMarijanovic.Areas.Identity.Data;
using SemRadIvanMarijanovic.Data;
using System.Globalization;

namespace SemRadIvanMarijanovic
{
    public class Program
    {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);
                var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

                builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

                builder.Services.Configure<IdentityOptions>(
                    options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequiredLength = 7;
                    }
                );

                // Add services to the container.
                builder.Services.AddControllersWithViews();

                builder.Services.AddRazorPages();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                var ci = new CultureInfo("de-De");

                ci.NumberFormat.NumberDecimalSeparator = ".";
                ci.NumberFormat.CurrencyDecimalSeparator = ".";

                app.UseRequestLocalization(
                    new RequestLocalizationOptions
                    {
                        DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(ci),
                        SupportedCultures = new List<CultureInfo> { ci },
                        SupportedUICultures = new List<CultureInfo> { ci }
                    }
                    );


                app.UseRouting();
                app.UseAuthentication(); ;

                app.UseAuthorization();

                app.UseAuthentication();

                app.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "admin/{controller}/{action}/{id?}"
                    );

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.MapRazorPages();


                app.Run();
            }
    }
    
}