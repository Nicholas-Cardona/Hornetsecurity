using System.Globalization;
using Hornet.Api.Services;
using Hornet.Data;
using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;
using Microsoft.EntityFrameworkCore;
public class LaunchService: ILaunchService
{
    private readonly AppDbContext _dbContext;

    public LaunchService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LaunchEntity?> GetLaunchByIdAsync(int id)
    {
        return await _dbContext.Launches.FindAsync(id);
    }

    public async Task<GetLastLaunchDto?> GetLastLaunchAsync()
    {
        return await _dbContext.Launches
        .OrderByDescending(l => l.Net)
        .Select(l => new GetLastLaunchDto
        {
            Id = l.Id,
            Name = l.Name,
            Slug = l.Slug,
            Net = l.Net,
            Rocket = new RocketDto 
            {
             Name=   l.Rocket.Name,
             Id = l.Rocket.Id,
             Variant = l.Rocket.Variant
            },
            LaunchServiceProvider = new LaunchServiceProviderDto
            {
                Id = l.LaunchServiceProvider.Id,
                Name = l.LaunchServiceProvider.Name
            },
            LaunchStatus = new LaunchStatusDto
            {
                Description = l.Status.Description,
                Id = l.Status.Id
            },
            ImageUrl = l.ImageUrl,
            WindowEnd = l.WindowEnd,
            WindowStart = l.WindowStart

        })
        .FirstOrDefaultAsync();

        
    }

    public async Task<IEnumerable<LaunchEntity>> GetLatestLaunchesAsync(int page, int size)
    {
        return await _dbContext.Launches.OrderByDescending(l => l.Net).Skip(size * (page -1)).Take(size).ToListAsync();
    }


}