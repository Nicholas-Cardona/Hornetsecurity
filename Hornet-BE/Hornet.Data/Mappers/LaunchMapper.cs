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
}