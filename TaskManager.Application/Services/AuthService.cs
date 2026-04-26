using TaskManager.Application.Contracts.Auth;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null || user.PasswordHash != request.Password)
            {
                throw new Exception("Invalid credentials");
            }

            return _jwtTokenService.GenerateToken(user);
        }

        public async Task<string> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var user = User.Create(
                Guid.NewGuid(),
                request.UserName,
                request.Email,
                request.Password // надо хешировать, сделать позже
            );

            await _userRepository.AddAsync(user, cancellationToken);

            return "User registered";
        }
    }
}
