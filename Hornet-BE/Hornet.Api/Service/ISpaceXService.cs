using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Api.Service;

public interface ISpaceXService
{
    public Task<SpaceXLaunchesResult> GetLatestLaunchesAsync();
}