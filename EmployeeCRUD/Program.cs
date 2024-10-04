using EmployeeCRUD.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

		
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			var app = builder.Build();


			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			else
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Employees}/{action=Index}/{id?}");
			    

            // Ensure the database is created and migrations are applied
            using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<ApplicationDbContext>();
					context.Database.Migrate();
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while migrating the database.");
				}
			}

			app.Run();
		}
	}
}