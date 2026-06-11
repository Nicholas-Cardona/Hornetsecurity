using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hornet.Data.Entities;

public class LaunchServiceProviderEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<LaunchEntity>? Launches { get; set; }
}