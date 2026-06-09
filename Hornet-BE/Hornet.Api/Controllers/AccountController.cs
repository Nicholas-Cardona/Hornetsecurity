using Hornet.Api.Service;
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
        catch
        {
            return BadRequest();
        }
    }
}
