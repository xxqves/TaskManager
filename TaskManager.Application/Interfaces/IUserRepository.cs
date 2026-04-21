using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(User user, CancellationToken cancellationToken = default);
    }
}
