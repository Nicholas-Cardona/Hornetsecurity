using System.Security.Authentication;
using Hornet.Data;
using Hornet.Data.Entities;
using Hornet.Data.Mappers;
using Hornet.Domain.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Api.Service;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly PasswordHasher<UserEntity> passwordHasher = new();

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> SignUpUserAsync(SignUpRequest request)
    {
        if (request.Password != request.PasswordConfirm) throw new ArgumentException("Mismatch between Password and Confirm Password");

        UserEntity user = UserMapper.FromRequest(request);

        string hashedPassword = passwordHasher.HashPassword(user, user.Password);

        user.Password = hashedPassword;

        var entry = await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<UserEntity> SignInUserAsync(SignInRequest request)
    {
        UserEntity? dbUser = await _context.Users.FirstOrDefaultAsync(i => i.Email == request.Email);

        if (dbUser == null) throw new KeyNotFoundException("User with the given email was not found");

        var result = passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, request.Password);

        if (result != PasswordVerificationResult.Success) throw new InvalidCredentialException("Invalid Password");

        return dbUser;
    }
}