using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Persistence.Entities;

namespace TaskManager.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Project project, CancellationToken cancellationToken = default)
        {
            var projectEntity = new ProjectEntity
            {
                Id = project.Id,
                Name = project.Name,
                OwnerId = project.OwnerId
            };

            await _context.AddAsync(projectEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return projectEntity.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _context.Projects
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return id;
        }

        public async Task<Project> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var projectEntity = await _context.Projects
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (projectEntity == null)
            {
                return null!;
            }

            var project = Project.Create(
                projectEntity!.Id,
                projectEntity.Name,
                projectEntity.OwnerId
            );

            return project;
        }

        public async Task<List<Project>> GetProjectsAsync(CancellationToken cancellationToken = default)
        {
            var projectsEntities = await _context.Projects
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var projects = projectsEntities
                .Select(x => Project.Create(
                    x.Id,
                    x.Name,
                    x.OwnerId))
                .ToList();

            return projects;
        }

        public async Task<List<Project>> GetProjectsByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var projectsEntities = await _context.Projects
                .AsNoTracking()
                .Where(x => x.OwnerId == ownerId)
                .ToListAsync(cancellationToken);

            var projects = projectsEntities
                .Select(x => Project.Create(
                    x.Id,
                    x.Name,
                    x.OwnerId))
                .ToList();

            return projects;
        }

        public async Task<Guid> UpdateAsync(Guid id, string name, Guid ownerId, CancellationToken cancellationToken = default)
        {
            await _context.Projects
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, r => name)
                    .SetProperty(r => r.OwnerId, r => ownerId), cancellationToken);

            return id;
        }
    }
}
