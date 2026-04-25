using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
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

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();

                dbContext!.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("swagger/index.html");
                }

                await next();
            });

            app.Run();
        }
    }
}
