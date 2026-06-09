using System.ComponentModel.DataAnnotations;

namespace Hornet.Domain.DTOs.Account;

public class UserModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }

    /// <summary>
    /// Must match the Password.
    /// </summary>
    public required string PasswordConfirm { get; set; }
}