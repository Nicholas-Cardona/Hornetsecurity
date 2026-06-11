using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Api.Service;

public class SpaceXService : ISpaceXService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "https://ll.thespacedevs.com/2.3.0/";
    public SpaceXService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Uri uri = new Uri(BaseUrl);
        _httpClient.BaseAddress = uri;
    }

    public async Task<SpaceXLaunchesResult> GetLatestLaunchesAsync()
    {
        var value = await _httpClient.GetAsync("launches?mode=list");

        var json = await value.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<SpaceXLaunchesResult>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

        return result;
    }
}