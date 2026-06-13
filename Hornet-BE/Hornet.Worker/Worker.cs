using System.Text;
using Hornet.Worker.Services;
using Hornet.Domain.DTOs.SpaceX;
using MySqlConnector;
using Quartz;

public class SpaceXSyncJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISpaceXSyncService _spaceXSyncService;
    private readonly ISpaceXService _spaceXService;

    public SpaceXSyncJob(IServiceScopeFactory scopeFactory, ISpaceXService spaceXService, ISpaceXSyncService spaceXSyncService)
    {
        _scopeFactory = scopeFactory;
        _spaceXService = spaceXService;
        _spaceXSyncService = spaceXSyncService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope();

        var upcomingTask = _spaceXService.GetLaunchesAsync(
            LaunchMode.Upcoming, 1, 20, true);

        var pastTask = _spaceXService.GetLaunchesAsync(
            LaunchMode.Past, 1, 20, true);

        await Task.WhenAll(upcomingTask, pastTask);

        await _spaceXSyncService.SyncLaunches(upcomingTask.Result);
        await _spaceXSyncService.SyncLaunches(pastTask.Result);
    }
}