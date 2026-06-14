using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using Hornet.Domain.DTOs.SpaceX;
using MySqlConnector;
using Org.BouncyCastle.Crypto.Prng;

namespace Hornet.Worker.Services;

public class SpaceXSyncService : ISpaceXSyncService
{
    private readonly ISpaceXService _spaceXService;
    private readonly MySqlDataSource _dataSource;

    public SpaceXSyncService(
        ISpaceXService spaceXService,
        MySqlDataSource dataSource)
    {
        _spaceXService = spaceXService;
        _dataSource = dataSource;
    }

    public async Task SyncLaunches(SpaceXLaunchesResult result)
    {
        await using var conn = await _dataSource.OpenConnectionAsync();
        await using var transaction = await conn.BeginTransactionAsync();

        // 1. Extract lookup data
        var rockets = result.Results
            .Where(x => x.Rocket?.Id != null)
            .Select(x => x.Rocket!)
            .DistinctBy(x => x.Id)
            .ToList();

        var statuses = result.Results
            .Where(x => x.Status?.Id != null)
            .Select(x => x.Status!)
            .DistinctBy(x => x.Id)
            .ToList();

        var providers = result.Results
            .Where(x => x.LaunchServiceProvider?.Id != null)
            .Select(x => x.LaunchServiceProvider!)
            .DistinctBy(x => x.Id)
            .ToList();

        await UpsertRockets(conn, transaction, rockets);
        await UpsertStatuses(conn, transaction, statuses);
        await UpsertLaunchServiceProviders(conn, transaction, providers);

        await UpsertLaunches(conn, transaction, result.Results);

        await transaction.CommitAsync();
    }

    private async Task UpsertLaunches(MySqlConnection conn,
        MySqlTransaction tx,
        List<SpaceXLaunch> launches)
    {
        if (launches.Count == 0)
            return;

        var sql = new StringBuilder();
        var parameters = new List<MySqlParameter>();

        sql.Append(@"
        INSERT INTO Launches (Id, Name, Slug, StatusId, Net, WindowStart, WindowEnd, ImageUrl, LaunchServiceProviderId, RocketId)
        VALUES
        ");

        for (int i = 0; i < launches.Count; i++)
        {
            var launch = launches[i];

            sql.Append($@"
            (@Id{i}, @Name{i}, @Slug{i}, @StatusId{i}, @Net{i}, @WindowStart{i}, @WindowEnd{i}, @ImageUrl{i}, @LaunchServiceProviderId{i}, @RocketId{i})");

            if (i < launches.Count - 1)
                sql.Append(",");

            parameters.Add(new MySqlParameter($"@Id{i}", launch.Id));
            parameters.Add(new MySqlParameter($"@Name{i}", launch.Name));
            parameters.Add(new MySqlParameter($"@Slug{i}", launch.Slug));
            parameters.Add(new MySqlParameter($"@StatusId{i}", launch.Status?.Id));
            parameters.Add(new MySqlParameter($"@Net{i}", launch.Net));
            parameters.Add(new MySqlParameter($"@WindowStart{i}", launch.WindowStart));
            parameters.Add(new MySqlParameter($"@WindowEnd{i}", launch.WindowEnd));
            parameters.Add(new MySqlParameter($"@ImageUrl{i}", launch.Image?.ImageUrl));
            parameters.Add(new MySqlParameter($"@LaunchServiceProviderId{i}", launch.LaunchServiceProvider?.Id));
            parameters.Add(new MySqlParameter($"@RocketId{i}", launch.Rocket?.Id));
        }

        sql.Append(@"
        ON DUPLICATE KEY UPDATE
        StatusId = VALUES(StatusId),
        WindowStart = VALUES(WindowStart),
        WindowEnd = VALUES(WindowEnd),
        ImageUrl = VALUES(ImageUrl);
        ");

        await using var cmd = conn.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = sql.ToString();


        cmd.Parameters.AddRange(parameters.ToArray());

        await cmd.ExecuteNonQueryAsync();
    }

    private async Task UpsertRockets(
        MySqlConnection conn,
        MySqlTransaction tx,
        List<Rocket> rockets)
    {
        if (rockets.Count == 0)
            return;

        var sql = new StringBuilder();
        var parameters = new List<MySqlParameter>();

        sql.Append(@"
        INSERT INTO Rockets (Id, Name, Variant)
        VALUES
        ");

        for (int i = 0; i < rockets.Count; i++)
        {
            var rocket = rockets[i];

            sql.Append($@"
            (@Id{i}, @Name{i}, @Variant{i})");

            if (i < rockets.Count - 1)
                sql.Append(",");

            parameters.Add(new MySqlParameter($"@Id{i}", rocket.Id));
            parameters.Add(new MySqlParameter($"@Name{i}", rocket.Configuration?.Name));
            parameters.Add(new MySqlParameter($"@Variant{i}", rocket.Configuration?.Variant));
        }

        sql.Append(@"
        ON DUPLICATE KEY UPDATE
            Name = VALUES(Name),
            Variant = VALUES(Variant);
        ");

        await using var cmd = conn.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = sql.ToString();
        cmd.Parameters.AddRange(parameters.ToArray());

        await cmd.ExecuteNonQueryAsync();
    }

    private async Task UpsertStatuses(
        MySqlConnection conn,
        MySqlTransaction tx,
        List<SpaceXStatus> statuses
    )
    {
        if (statuses.Count == 0) return;

        var sql = new StringBuilder();
        var parameters = new List<MySqlParameter>();

        sql.Append(@"
            INSERT INTO LaunchStatuses (Id, Name, Description)
            VALUES 
        ");

        for (int i = 0; i < statuses.Count; i++)
        {
            var status = statuses[i];

            sql.Append($@"
             (@Id{i}, @Name{i}, @Description{i})
            ");

            if (i < statuses.Count - 1) sql.Append(",");

            parameters.Add(new MySqlParameter($"@Id{i}", status.Id));
            parameters.Add(new MySqlParameter($"@Name{i}", status.Name));
            parameters.Add(new MySqlParameter($"@Description{i}", status.Description));
        }

        sql.Append(@"
        ON DUPLICATE KEY UPDATE 
        Name=VALUES(Name), 
        Description=VALUES(Description);
        ");

        await using var cmd = conn.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = sql.ToString();
        cmd.Parameters.AddRange(parameters.ToArray());

        await cmd.ExecuteNonQueryAsync();
    }

    private async Task UpsertLaunchServiceProviders(
      MySqlConnection conn,
      MySqlTransaction tx,
      List<LaunchServiceProvider> providers
  )
    {
        if (providers.Count == 0)
            return;

        var sql = new StringBuilder();
        var parameters = new List<MySqlParameter>();

        sql.Append("INSERT INTO LaunchServiceProviders (Id, Name) VALUES ");

        for (int i = 0; i < providers.Count; i++)
        {
            var provider = providers[i];

            if (i > 0)
                sql.Append(",");

            sql.Append($"(@Id{i}, @Name{i})");

            parameters.Add(new MySqlParameter($"@Id{i}", provider.Id));
            parameters.Add(new MySqlParameter($"@Name{i}", provider.Name ?? (object)DBNull.Value));
        }

        sql.Append(@"
        ON DUPLICATE KEY UPDATE
            Name = VALUES(Name);
    ");

        await using var cmd = conn.CreateCommand();
        cmd.Transaction = tx;
        cmd.CommandText = sql.ToString();
        cmd.Parameters.AddRange(parameters.ToArray());

        await cmd.ExecuteNonQueryAsync();
    }
}