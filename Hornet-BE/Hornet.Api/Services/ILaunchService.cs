using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Api.Services;

public interface ILaunchService
{
     public Task<LaunchEntity?> GetLaunchByIdAsync(int id);
     public Task<GetLaunchDto?> GetLastLaunchAsync();
     public Task<IEnumerable<GetLaunchDto>> GetLatestLaunchesAsync(LaunchMode mode, int page, int size);
     
}