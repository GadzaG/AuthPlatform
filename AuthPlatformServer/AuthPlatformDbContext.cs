using Microsoft.EntityFrameworkCore;
using AuthPlatformServer.Models;
using AuthPlatformServer.Configurations;

public class AuthPlatformDbContext(DbContextOptions<AuthPlatformDbContext> options) :DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<KeyEntity> Keys { get; set; }

    public DbSet<GlobalEntity> Globals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new KeyConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalConfiguration());
    }
}
