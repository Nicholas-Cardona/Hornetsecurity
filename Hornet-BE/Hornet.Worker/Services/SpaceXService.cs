using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using Hornet.Domain.DTOs.SpaceX;
using Microsoft.AspNetCore.WebUtilities;

namespace Hornet.Worker.Services;

public class SpaceXService : ISpaceXService
{
    private readonly HttpClient _httpClient;
    private readonly string BaseUrl = "https://lldev.thespacedevs.com/2.3.0/";
    public SpaceXService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Uri uri = new Uri(BaseUrl);
        _httpClient.BaseAddress = uri;
    }

    public async Task<SpaceXLaunchesResult> GetLaunchesAsync(LaunchMode mode, int page, int size, bool desc)
    {
        if (page < 1 || size < 1) throw new ArgumentException("Page and Size must both be larger than 0");

        int offset = (page - 1) * size;
        string ordering = desc ? "-net" : "net";

        Dictionary<string, string?> queryParams = new()
        {
            ["offset"] = offset.ToString(),
            ["ordering"] = ordering,
            ["limit"] = size.ToString(),
            ["lsp__name"] = "SpaceX"
        };

        string endpoint = mode switch
        {
            LaunchMode.Upcoming => "launches/upcoming/",
            LaunchMode.Past => "launches/previous/",
            _ => "launches/"
        };

        string url = QueryHelpers.AddQueryString(endpoint, queryParams);

        var value = await _httpClient.GetAsync(url);

        var json = await value.Content.ReadAsStringAsync();

        try
        {
            var result = JsonSerializer.Deserialize<SpaceXLaunchesResult>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (result == null) throw new Exception("Deserialization returned null");

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to deserialize SpaceX response", ex);
        }
    }

    public async Task<SpaceXLaunch> GetLatestLaunchAsync()
    {
        var launches = await GetLaunchesAsync(LaunchMode.Past, 1, 1, true);

        var lastLaunch = launches.Results.FirstOrDefault() ?? throw new KeyNotFoundException("No launches found");

        return lastLaunch;
    }
}