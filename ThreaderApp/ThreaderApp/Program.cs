using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThreaderApp.Models;
using ThreaderApp.Data;

namespace ThreaderApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add the database context
			builder.Services.AddDbContext<ThreaderContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			// Add Identity
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<ThreaderContext>()
				.AddDefaultTokenProviders();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

            var app = builder.Build();

			// Ensure database is created and migrated
			using (var scope = app.Services.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<ThreaderContext>();
				dbContext.Database.Migrate();
			}

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			app.UseAuthentication(); // Enable Identity authentication
			app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
