using System.ComponentModel.DataAnnotations;

namespace Hornet.Domain.DTOs.Account;


public class SignInRequest
{
    private string _email = string.Empty;

    [EmailAddress]
    public required string Email
    {
        get => _email;
        set => _email = value.Trim();
    }
    public required string Password { get; set; }
    public required bool RememberMe {get;set;}
}