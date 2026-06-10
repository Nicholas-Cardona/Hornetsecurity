using Microsoft.AspNetCore.Identity;

namespace Hornet.Data.Entities;

public class UserEntity : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Password { get; set; }
}