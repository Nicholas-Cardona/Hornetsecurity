namespace Hornet.Domain.DTOs.Account
{
    public class UserResponse
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public required string Email { get; set; }
        public required string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public bool TwoFAEnabled { get; set; }
    }
}