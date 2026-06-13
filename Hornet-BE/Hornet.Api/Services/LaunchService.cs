using System.Globalization;
using System.Text;
using Hornet.Api.Services;
using Hornet.Data;
using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
public class LaunchService : ILaunchService
{
    private readonly AppDbContext _dbContext;
    private readonly MySqlDataSource _dataSource;

    public LaunchService(AppDbContext dbContext, MySqlDataSource dataSource)
    {
        _dbContext = dbContext;
        _dataSource = dataSource;
    }

    public async Task<LaunchEntity?> GetLaunchByIdAsync(int id)
    {
        return await _dbContext.Launches.FindAsync(id);
    }

    public async Task<GetLaunchDto?> GetLastLaunchAsync()
    {
        return await _dbContext.Launches
        .OrderByDescending(l => l.Net)
        .Select(l => new GetLaunchDto
        {
            Id = l.Id,
            Name = l.Name,
            Slug = l.Slug,
            Net = l.Net,
            Rocket = new RocketDto
            {
                Name = l.Rocket.Name,
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

    public async Task<IEnumerable<GetLaunchDto>> GetLatestLaunchesAsync(
      int page,
      int size)
    {
        var offset = (page - 1) * size;

        const string sql = """
        SELECT
            l.Id,
            l.Name,
            l.Net,
            l.Slug,
            l.ImageUrl,
            l.WindowEnd,
            l.WindowStart,
            l.RocketId,
            l.LaunchServiceProviderId,
            l.StatusId,
            ls.Name as LaunchStatusName,
            ls.Description as LaunchStatusDescription,
            r.Name as RocketName,
            r.Variant as RocketVariant,
            lsp.Name as LSPName
        FROM Launches l
       
        LEFT JOIN LaunchStatuses ls
        ON l.StatusId = ls.Id
      
        LEFT JOIN Rockets r
        ON l.RocketId = r.Id
        
        LEFT JOIN LaunchServiceProviders lsp
        ON lsp.Id = l.LaunchServiceProviderId

        ORDER BY l.Net DESC
        LIMIT @size OFFSET @offset;
        """;

        await using var conn = await _dataSource.OpenConnectionAsync();
        await using var cmd = conn.CreateCommand();

        cmd.CommandText = sql;
        cmd.Parameters.AddWithValue("@size", size);
        cmd.Parameters.AddWithValue("@offset", offset);

        await using var reader = await cmd.ExecuteReaderAsync();

        var launches = new List<GetLaunchDto>();

        while (await reader.ReadAsync())
        {
            launches.Add(new GetLaunchDto
            {
                Id = reader.GetGuid("Id"),
                Name = reader.GetString("Name"),
                Net =  reader.GetDateTime("Net"),
                Rocket = new RocketDto
                {
                  Id = reader.GetInt32("RocketId"),
                  Name = reader.GetString("RocketName"),
                  Variant = reader.GetString("RocketVariant")  
                },
                ImageUrl = reader.GetString("ImageUrl"),
                LaunchServiceProvider = new LaunchServiceProviderDto
                {
                  Id = reader.GetInt32("LaunchServiceProviderId"),
                  Name = reader.GetString("LSPName"),
                  
                },
                LaunchStatus = new LaunchStatusDto
                {
                Id = reader.GetInt32("StatusId") ,
                Description = reader.GetString("LaunchStatusDescription"),
                Name = reader.GetString("LaunchStatusName")
                },
                WindowEnd = reader.GetDateTime("WindowEnd"),
            });
        }

        return launches;
    }


}