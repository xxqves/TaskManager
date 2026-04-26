using Microsoft.EntityFrameworkCore;
using TaskManager.API.Configuration;
using TaskManager.Persistence;

namespace TaskManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddConfiguration(builder.Configuration);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                dbContext!.Database.Migrate();
            }

            app.UseApplicationMiddleware();

            app.Run();
        }
    }
}
