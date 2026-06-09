using Hornet.Data;
using Hornet.Data.Mappers;
using Hornet.Data.Models;
using Hornet.Domain.DTOs.Account;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Api.Service;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> SignUpUserAsync(SignUpRequest request)
    {
        if (request.Password != request.PasswordConfirm) throw new ArgumentException("Mismatch between Password and Confirm Password");

        User user = UserMapper.FromRequest(request);

        var entry = await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<User> SignInUserAsync(SignInRequest request)
    {
        User? dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Email == request.Email);

        if (dbUser == null) throw new KeyNotFoundException("User with the given email was not found");

        if (dbUser.Password != request.Password) throw new UnauthorizedAccessException("Invalid Password");

        return dbUser;
    }
}