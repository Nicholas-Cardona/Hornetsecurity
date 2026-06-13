using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    [HttpGet("latest", Name = "Latest Launches")]
    public async Task<IActionResult> GetPastLaunches([FromQuery] GetLatestLaunchesRequest request)
    {
        try
        {
            var res = await _launchService.GetLaunchesAsync(LaunchMode.Past, request.Page, request.Size);

            return Ok(res);
        }
        catch (ArgumentException)
        {
            return BadRequest("One of the provided parameters was incorrect");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Uncaught Error");
        }
    }

    [HttpGet("count", Name = "Launch Count")]
    public async Task<IActionResult> GetLaunchesCount([Required] LaunchMode mode)
    {
        try
        {
            var res = await _launchService.GetLaunchesCount(mode);
            
            return Ok(res);
        }
        catch
        {
            return StatusCode(500, "Uncaught Error");
        }
    }

    [HttpGet("upcoming", Name = "Upcoming Launches")]
    public async Task<IActionResult> GetUpcomingLaunches([FromQuery] GetUpcomingLaunchesRequest request)
    {
        try
        {
            var res = await _launchService.GetLaunchesAsync(LaunchMode.Upcoming, request.Page, request.Size);

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