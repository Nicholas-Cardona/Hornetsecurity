using Hornet.Api.Service;
using Hornet.Data.Mappers;
using Hornet.Domain.DTOs.SpaceX;
using Quartz;

public class SpaceXSyncJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SpaceXSyncJob(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        using var scope = _scopeFactory.CreateScope();

        var spaceXService = scope.ServiceProvider.GetRequiredService<ISpaceXService>();

        var result = await spaceXService.GetLaunchesAsync(
           LaunchMode.All,
            page: 1,
            size: 10,
            desc: true
        );

        foreach (var launch in result.Results)
        {
            var entity = LaunchMapper.ToEntity(launch);
            Console.WriteLine(entity.Name);
        }
    }
}