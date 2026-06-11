using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hornet.Data.Entities;

public class RocketEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Variant { get; set; }

    public List<LaunchEntity>? Launches { get; set; }
}