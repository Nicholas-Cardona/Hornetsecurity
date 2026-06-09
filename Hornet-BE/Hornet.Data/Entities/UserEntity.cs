using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    /// <summary>
    /// Primary key for the user.
    /// Uses a GUID to ensure global uniqueness and to avoid coupling
    /// relationships to mutable user data such as email addresses.
    /// </summary>
    [Key]
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}