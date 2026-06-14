using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Entities;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Data.Mappers;

public class LaunchMapper
{
    public static LaunchEntity ToEntity(SpaceXLaunch dto)
    {
        return new LaunchEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Slug = dto.Slug,
            StatusId = dto.Status?.Id,
            Net = dto.Net,
            WindowStart = dto.WindowStart,
            WindowEnd = dto.WindowEnd,
            ImageUrl = dto.Image?.ImageUrl,
            RocketId = dto.Rocket?.Id,
            LaunchServiceProviderId = dto.LaunchServiceProvider?.Id
        };
    }

    public static GetLaunchDto ToDto(LaunchEntity l)
    {
        return new GetLaunchDto
        {
            Id = l.Id,
            Name = l.Name,
            Slug = l.Slug,
            Net = l.Net,
            Rocket = new RocketDto
            {
                Name = l.Rocket != null ? l.Rocket.Name : "N/A",
                Id = l.Rocket != null ? l.Rocket.Id : 0,
                Variant = l.Rocket != null ? l.Rocket.Variant : "N/A"
            },
            LaunchServiceProvider = new LaunchServiceProviderDto
            {
                Id = l.LaunchServiceProvider != null ? l.LaunchServiceProvider.Id : 0,
                Name = l.LaunchServiceProvider != null ? l.LaunchServiceProvider.Name : "N/A"
            },
            LaunchStatus = new LaunchStatusDto
            {
                Description = l.Status != null ? l.Status.Description : "N/A",
                Name = l.Status != null ? l.Status.Name : "N/A",
                Id = l.Status != null ? l.Status.Id : 0
            },
            ImageUrl = l.ImageUrl,
            WindowEnd = l.WindowEnd,
            WindowStart = l.WindowStart

        };
    }
}