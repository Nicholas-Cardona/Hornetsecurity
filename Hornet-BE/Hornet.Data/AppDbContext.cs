using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
}