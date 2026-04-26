using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
