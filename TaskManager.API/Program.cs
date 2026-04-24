using Microsoft.EntityFrameworkCore;
using TaskManager.Persistence;

namespace TaskManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                dbContext!.Database.Migrate();
            }

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
