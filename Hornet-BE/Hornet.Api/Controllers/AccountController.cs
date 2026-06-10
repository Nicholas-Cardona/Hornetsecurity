using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using Hornet.Api.Service;
using Hornet.Data.Entities;
using Hornet.Domain.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace Hornet.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost("sign-up", Name = "SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        try
        {
            await _service.SignUpUserAsync(request);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest("Password and PasswordConfirm do NOT match");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest();
        }
    }

    [HttpGet("sign-in", Name = "SignIn")]
    public async Task<IActionResult> SignIn([FromQuery] SignInRequest request)
    {
        try
        {
            await _service.SignInUserAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            switch (e)
            {
                // Not providing any additional information to the user on purpose, just to not expose why the login failed to
                // potential hackers
                case InvalidCredentialException:
                case KeyNotFoundException:
                    return Unauthorized("Invalid Email or Password");

                default:
                    return StatusCode(500);
            }
        }
    }
}
