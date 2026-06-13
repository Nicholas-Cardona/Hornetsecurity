namespace Hornet.Domain.DTOs.SpaceX;
public class GetLastLaunchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Slug { get; set; }
    public LaunchStatusDto LaunchStatus {get;set;}
    public DateTime? WindowStart { get; set; }
    public DateTime? WindowEnd { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime? Net { get; set; }

    public RocketDto Rocket { get; set; }
    public LaunchServiceProviderDto LaunchServiceProvider { get;set;}
}
