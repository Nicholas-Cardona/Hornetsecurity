using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Worker.Services;

public interface ISpaceXService
{
    public Task<SpaceXLaunchesResult> GetLaunchesAsync(LaunchMode mode, int page, int size, bool desc);
    public Task<SpaceXLaunch> GetLatestLaunchAsync();
}