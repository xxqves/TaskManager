using TaskManager.Application.Contracts.Auth;

namespace TaskManager.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

        Task<string> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
