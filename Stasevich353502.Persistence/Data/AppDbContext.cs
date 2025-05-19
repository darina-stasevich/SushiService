using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Stasevich353502.Persistence.Data;

public sealed class AppDbContext : DbContext
{
    public DbSet<SushiSet> SushiSets { get; set; } = null!;
    public DbSet<Sushi> Sushi { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sushi>().OwnsOne<SushiData>(s => s.SushiData);
    }
}