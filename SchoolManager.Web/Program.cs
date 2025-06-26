
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Application;
using SchoolManager.Application.ViewModels.Employee;
using SchoolManager.Infrastructure;

namespace SchoolManager.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var runMigrations = builder.Configuration.GetValue<bool>("RunMigrations");

            Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "Logs"));
            builder.Host.UseSerilog((ctx, lc) =>
            {
                lc.MinimumLevel.Information()
                    .WriteTo.Async(a => a.File("Logs/myLog-.txt", rollingInterval: RollingInterval.Day))
                    .ReadFrom.Configuration(ctx.Configuration);
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddTransient<IValidator<NewEmployeeVm>, NewEmployeeVmValidator>();

            builder.Services.AddApplication(); 
            builder.Services.AddInfrastructure(); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() && runMigrations)
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
