using CrockeryFactory.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace CrockeryFactory.Desktop.Data;

public class FactoryDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductionBatch> Batches => Set<ProductionBatch>();

    /// <summary>
    /// The SQLite file lives in %LOCALAPPDATA%\CrockeryFactory so a rebuild never wipes your data.
    /// Delete the folder if you want to start over from the seed data.
    /// </summary>
    public static string DatabasePath
    {
        get
        {
            var folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CrockeryFactory");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "factory.db");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DatabasePath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Sku).IsUnique();
            entity.Property(p => p.Sku).HasMaxLength(16).IsRequired();
            entity.Property(p => p.Name).HasMaxLength(120).IsRequired();
            entity.Property(p => p.GlazeColour).HasMaxLength(60);
            entity.Property(p => p.UnitPrice).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.HasIndex(b => b.BatchCode).IsUnique();
            entity.Property(b => b.BatchCode).HasMaxLength(24).IsRequired();
            entity.Property(b => b.KilnId).HasMaxLength(12);

            entity.HasOne(b => b.Product)
                  .WithMany(p => p.Batches)
                  .HasForeignKey(b => b.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
