using System.Text;
using Hornet.Api.Service;
using Hornet.Data.Mappers;
using Hornet.Domain.DTOs.SpaceX;
using Hornet.Worker.Services;
using MySqlConnector;
using Org.BouncyCastle.Cms;
using Quartz;

public class SpaceXSyncJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISpaceXSyncService _spaceXSyncService;

    public SpaceXSyncJob(IServiceScopeFactory scopeFactory, MySqlDataSource dataSource, ISpaceXSyncService spaceXSyncService)
    {
        _scopeFactory = scopeFactory;
        _spaceXSyncService = spaceXSyncService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope();

        var spaceXService = scope.ServiceProvider.GetRequiredService<ISpaceXService>();

        await _spaceXSyncService.SyncLatestLaunches();
   }

    private async void SaveRockets()
    {

    }
}