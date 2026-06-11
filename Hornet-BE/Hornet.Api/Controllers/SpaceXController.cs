using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Api.Service;
using Hornet.Domain.DTOs.SpaceX;
using Microsoft.AspNetCore.Mvc;

namespace Hornet.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SpaceXController : ControllerBase
{
    private readonly ISpaceXService _spaceXService;

    public SpaceXController(ISpaceXService spaceXService)
    {
        _spaceXService = spaceXService;
    }

    [HttpGet("latest", Name = "Latest Lunches")]
    public async Task<IActionResult> GetPastLaunches([FromQuery] GetLatestLaunchesRequest request)
    {
        try
        {
            var res = await _spaceXService.GetLaunchesAsync(LaunchMode.Past, request.Page, request.Size, true);

            return Ok(res);
        }
        catch (ArgumentException)
        {
            return BadRequest("One of the provided parameters was incorrect");
        }
        catch
        {
            return StatusCode(500, "Uncaught Error");
        }
    }

    [HttpGet("upcoming", Name = "Upcoming Lunches")]
    public async Task<IActionResult> GetUpcomingLaunches([FromQuery] GetUpcomingLaunchesRequest request)
    {
        try
        {
            var res = await _spaceXService.GetLaunchesAsync(LaunchMode.Upcoming, request.Page, request.Size, false);

            return Ok(res);
        }
        catch (ArgumentException)
        {
            return BadRequest("One of the provided parameters was incorrect");
        }
        catch
        {
            return StatusCode(500, "Uncaught Error");
        }
    }

}