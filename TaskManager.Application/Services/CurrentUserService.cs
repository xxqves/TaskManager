using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public string Email =>
            _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;

        public string Role =>
            _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Role)!.Value;
    }
}
