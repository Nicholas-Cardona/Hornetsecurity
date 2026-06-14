using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Api.Services;

public interface ILaunchService
{
     public Task<LaunchEntity?> GetLaunchByIdAsync(int id);
     public Task<GetLaunchDto?> GetLastLaunchAsync();
     public Task<IEnumerable<GetLaunchDto>> GetLaunchesAsync(LaunchMode mode, int page, int size, bool desc);
      public Task<int> GetLaunchesCount(LaunchMode mode);
}