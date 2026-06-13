using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Api.Services;
using Hornet.Domain.DTOs.SpaceX;
using Microsoft.AspNetCore.Mvc;

namespace Hornet.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SpaceXController : ControllerBase
{
    private readonly ILaunchService  _launchService;

    public SpaceXController(ILaunchService launchService)
    {
        _launchService = launchService;
    }

    [HttpGet("latest", Name = "Latest Lunches")]
    public async Task<IActionResult> GetPastLaunches([FromQuery] GetLatestLaunchesRequest request)
    {
        try
        {
            var res = await _launchService.GetLatestLaunchesAsync(request.Page, request.Size);

            return Ok(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Uncaught Error");
        }
    }

    // [HttpGet("upcoming", Name = "Upcoming Lunches")]
    // public async Task<IActionResult> GetUpcomingLaunches([FromQuery] GetUpcomingLaunchesRequest request)
    // {
    //     try
    //     {
    //         var res = await _spaceXService.GetLaunchesAsync(LaunchMode.Upcoming, request.Page, request.Size, false);

    //         return Ok(res);
    //     }
    //     catch (ArgumentException)
    //     {
    //         return BadRequest("One of the provided parameters was incorrect");
    //     }
    //     catch
    //     {
    //         return StatusCode(500, "Uncaught Error");
    //     }
    // }

    [HttpGet("last", Name = "Most Recent Launch")]
    public async Task<IActionResult> GetMostRecentLaunch()
    {
        try
        {
            var launch = await _launchService.GetLastLaunchAsync();

            return Ok(launch);
        }
        catch
        {
            return StatusCode(500);
        }
    }
}