namespace Hornet.Data.Entities;

public class LaunchEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }

    public int? StatusId { get; set; }
    public LaunchStatusEntity? Status { get; set; }

    public DateTime? Net { get; set; }
    public DateTime? WindowStart { get; set; }
    public DateTime? WindowEnd { get; set; }
    public string? ImageUrl { get; set; }

    public int? LaunchServiceProviderId { get; set; }
    public LaunchServiceProviderEntity? LaunchServiceProvider { get; set; }

    public int? RocketId { get; set; }
    public RocketEntity? Rocket { get; set; }
}