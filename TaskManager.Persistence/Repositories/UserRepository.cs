using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Persistence.Entities;

namespace TaskManager.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash
            };

            await _context.AddAsync(userEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return userEntity.Id;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            if (userEntity == null)
            {
                return null!;
            }

            var user = User.Create(
                userEntity!.Id,
                userEntity.UserName,
                userEntity.Email,
                userEntity.PasswordHash
            );

            return user;
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (userEntity == null)
            {
                return null!;
            }

            var user = User.Create(
                userEntity!.Id,
                userEntity.UserName,
                userEntity.Email,
                userEntity.PasswordHash
            );

            return user;
        }
    }
}
