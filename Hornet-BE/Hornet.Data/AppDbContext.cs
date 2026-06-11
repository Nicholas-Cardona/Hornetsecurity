using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hornet.Data;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<LaunchEntity> Launches => Set<LaunchEntity>();
    public DbSet<RocketEntity> Rockets => Set<RocketEntity>();
    public DbSet<LaunchStatusEntity> LaunchStatuses => Set<LaunchStatusEntity>();
    public DbSet<LaunchServiceProviderEntity> LaunchServiceProviders => Set<LaunchServiceProviderEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LaunchEntity>(entity =>
        {
            entity.HasOne(x => x.Rocket)
                  .WithMany(r => r.Launches)
                  .HasForeignKey(x => x.RocketId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Status)
                  .WithMany(s => s.Launches)
                  .HasForeignKey(x => x.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.LaunchServiceProvider)
                  .WithMany(p => p.Launches)
                  .HasForeignKey(x => x.LaunchServiceProviderId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}