using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Data;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}