using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hornet.Worker.Services;

public interface ISpaceXSyncService
{
    public Task SyncLatestLaunches();
}