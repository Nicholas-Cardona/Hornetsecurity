using System.ComponentModel.DataAnnotations;

namespace Hornet.Domain.DTOs.Account;

public class SignUpRequest
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;

    public required string FirstName
    {
        get => _firstName;
        set => _firstName = value.Trim();
    }

    public required string LastName
    {
        get => _lastName;
        set => _lastName = value.Trim();
    }

    [EmailAddress]
    public required string Email
    {
        get => _email;
        set => _email = value.Trim();
    }
    public required string Password { get; set; }

    /// <summary>
    /// Must match the Password.
    /// </summary>
    public required string PasswordConfirm { get; set; }
}