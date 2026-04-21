namespace TaskManager.Domain.Models
{
    public class User
    {
        public const int MAX_USERNAME_LENGTH = 18;

        public Guid Id { get; }

        public string UserName { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string PasswordHash { get; } = string.Empty;

        public string Role { get; } = "User";

        private User(Guid id, string userName, string email, string passwordHash)
        {
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static User Create(Guid id, string userName, string email, string passwordHash)
        {
            if (userName.Length > MAX_USERNAME_LENGTH)
            {
                throw new ArgumentException($"Username must not exceed {MAX_USERNAME_LENGTH} characters.");
            }

            return new User(id, userName, email, passwordHash);
        }
    }
}