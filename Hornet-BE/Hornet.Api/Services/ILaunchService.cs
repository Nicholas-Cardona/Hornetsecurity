using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Api.Services;

public interface ILaunchService
{
     public Task<LaunchEntity?> GetLaunchByIdAsync(int id);
     public Task<GetLastLaunchDto?> GetLastLaunchAsync();
     public Task<IEnumerable<LaunchEntity>> GetLatestLaunchesAsync(int page, int size);
     
}