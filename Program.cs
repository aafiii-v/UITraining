using Microsoft.EntityFrameworkCore;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

		builder.Services.AddDbContext<ApplicationContext>(
			dbContextOptions => dbContextOptions
				.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"), serverVersion)
				.LogTo(Console.WriteLine, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.EnableDetailedErrors()
		);

        builder.Services.AddScoped<IProduct, ProductServices>();

		// Add services to the container.
		builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");

        app.Run();
    }
}