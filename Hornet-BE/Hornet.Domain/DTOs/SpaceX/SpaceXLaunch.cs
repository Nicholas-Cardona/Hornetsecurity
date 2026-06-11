using System.Data.Common;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Hornet.Domain.DTOs.SpaceX;

public class SpaceXLaunch
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("response_mode")]
    public string ResponseMode { get; set; }

    public string Slug { get; set; }

    [JsonPropertyName("launch_designator")]
    public string LaunchDesignator { get; set; }

    public SpaceXStatus Status { get; set; }

    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }

    public DateTime Net { get; set; }

    [JsonPropertyName("net_precision")]
    public string NetPrecision { get; set; }

    [JsonPropertyName("window_end")]
    public DateTime WindowEnd { get; set; }

    [JsonPropertyName("window_start")]
    public DateTime WindowStart { get; set; }

    public SpaceXImage Image { get; set; }
    public object Infographic { get; set; }

    public int? Probability { get; set; }

    [JsonPropertyName("weather_concerns")]
    public string WeatherConcerns { get; set; }

    public string Failreason { get; set; }
    public string Hashtag { get; set; }

    [JsonPropertyName("launch_service_provider")]
    public LaunchServiceProvider LaunchServiceProvider { get; set; }

    public Rocket Rocket { get; set; }
    public Mission Mission { get; set; }
    public Pad Pad { get; set; }

    [JsonPropertyName("webcast_live")]
    public bool WebcastLive { get; set; }

    public List<object> Program { get; set; }

}

public class SpaceXStatus
{
    public int Id { get; set; }
public string Name { get; set; }
public string Abbrev { get; set; } 
public string Description { get; set; }
}

public class SpaceXImage
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("thumbnail_url")]
    public string ThumbnailUrl { get; set; }

    public string Credit { get; set; }
    public ImageLicense License { get; set; }

    [JsonPropertyName("single_use")]
    public bool SingleUse { get; set; }

    public List<object> Variants { get; set; }
}

public class ImageLicense
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public string Link { get; set; }
}

public class LaunchServiceProvider
{
    [JsonPropertyName("response_mode")]
    public string ResponseMode { get; set; }

    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string Abbrev { get; set; }

    public AgencyType Type { get; set; }
}

public class AgencyType
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Rocket
{
    public int Id { get; set; }
    public RocketConfiguration Configuration { get; set; }
}

public class RocketConfiguration
{
    [JsonPropertyName("response_mode")]
    public string ResponseMode { get; set; }

    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Variant { get; set; }

    public List<RocketFamily> Families { get; set; }
}

public class RocketFamily
{
    [JsonPropertyName("response_mode")]
    public string ResponseMode { get; set; }

    public int Id { get; set; }
    public string Name { get; set; }
}

public class Mission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public object Image { get; set; }

    public Orbit Orbit { get; set; }

    public List<object> Agencies { get; set; }
    public List<object> InfoUrls { get; set; }
    public List<object> VidUrls { get; set; }
}

public class Orbit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abbrev { get; set; }
}

public class Pad
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    [JsonPropertyName("map_url")]
    public string MapUrl { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public Country Country { get; set; }

    [JsonPropertyName("map_image")]
    public string MapImage { get; set; }

    [JsonPropertyName("total_launch_count")]
    public int TotalLaunchCount { get; set; }

    public Location Location { get; set; }
}

public class Location
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
}

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonPropertyName("alpha_2_code")]
    public string Alpha2Code { get; set; }

    [JsonPropertyName("alpha_3_code")]
    public string Alpha3Code { get; set; }

    [JsonPropertyName("nationality_name")]
    public string NationalityName { get; set; }
}