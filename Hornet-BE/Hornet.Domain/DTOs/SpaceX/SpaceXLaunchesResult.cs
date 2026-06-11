using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hornet.Domain.DTOs.SpaceX;

public class SpaceXLaunchesResult
{
    public int Count {get;set;}
    public string Next {get;set;} = string.Empty;
    public string Previous {get;set;} = string.Empty;
    public List<SpaceXLaunch> Results {get;set;} = new ();
}
