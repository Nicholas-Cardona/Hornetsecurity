namespace Hornet.Domain.DTOs.SpaceX;
public class GetLaunchDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Slug { get; set; }
    public required LaunchStatusDto LaunchStatus {get;set;}
    public DateTime? WindowStart { get; set; }
    public DateTime? WindowEnd { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime? Net { get; set; }

    public required RocketDto Rocket { get; set; }
    public required LaunchServiceProviderDto LaunchServiceProvider { get;set;}
}
