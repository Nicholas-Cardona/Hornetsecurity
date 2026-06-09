using Microsoft.AspNetCore.Mvc;

namespace Hornet.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AccountController : ControllerBase
{
    [HttpPost(Name = "SignUp")]
    public IActionResult SignUp()
    {
        return Ok();
    }
}
