using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hornet.Domain.DTOs.Shared;

public class PagedRequest
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; }
    [Range(1, 50)]
    public int Size { get; set; }
}