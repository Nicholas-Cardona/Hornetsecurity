using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Domain.DTOs.SpaceX;

namespace Hornet.Worker.Services;

public interface ISpaceXSyncService
{
    public Task SyncLaunches(SpaceXLaunchesResult result);
}