using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Claims;
using Hornet.Data.Entities;
using Hornet.Data.Mappers;
using Hornet.Domain.DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Hornet.Api.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IHttpContextAccessor httpAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpAccessor;
    }

    public async Task SignUpUserAsync(SignUpRequest request)
    {
        if (request.Password != request.PasswordConfirm) throw new ArgumentException("Mismatch between Password and Confirm Password");

        UserEntity user = UserMapper.FromRequest(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            ParseIdentityErrors(result.Errors);
        }

        await SignInUserAsync(new SignInRequest { Email = request.Email, Password = request.Password, RememberMe = false });
    }

    public async Task SignInUserAsync(SignInRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            throw new InvalidCredentialException("Invalid login");

        var result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            request.RememberMe,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new InvalidCredentialException("Invalid login");
        }
    }

    public async Task<UserEntity> GetIdentity()
    {
        var userContext = _httpContextAccessor.HttpContext?.User;

        if (userContext is null)
        {
            throw new KeyNotFoundException("User is not logged in, thus not cookie set in HttpContext");
        }

        var user = await _userManager.GetUserAsync(userContext);

        if (user is not null)
        {
            return user;
        }

        throw new KeyNotFoundException("User Not Found");
    }

    private void ParseIdentityErrors(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
        {
            switch (error.Code)
            {
                case "PasswordRequiresUpper":
                    throw new ValidationException("Requires Uppercase");

                case "DuplicateUserName":
                    throw new ValidationException("Username already in use");

                case "PasswordRequiresNonAlphanumeric":
                    throw new ValidationException("Missing Special Character");

                case "PasswordRequiresLower":
                    throw new ValidationException("Missing Lowercase");

                case "PasswordTooShort":
                    throw new ValidationException("Password is too short");

                case "DuplicateEmail":
                    throw new ValidationException("Email already in use");
            }
        }

        throw new Exception("Invalid Sign Up");
    }
}