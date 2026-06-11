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
    public async Task<SpaceXLaunchesResult> GetLatestLaunches()
    {
        var res = await _spaceXService.GetLatestLaunchesAsync();

        return res;
    }
}