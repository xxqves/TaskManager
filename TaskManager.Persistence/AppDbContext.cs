using Microsoft.EntityFrameworkCore;
using TaskManager.Persistence.Entities;

namespace TaskManager.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

        public DbSet<TaskItemEntity> Tasks => Set<TaskItemEntity>();
    }
}
