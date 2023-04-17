using Microsoft.EntityFrameworkCore;
using RSApp.Core.Domain.Core;
using RSApp.Core.Domain.Entities;

namespace RSApp.Infrastructure.Persistence.Context;

public class RSAppContext : DbContext {
  public RSAppContext(DbContextOptions<RSAppContext> options) : base(options) { }

  public DbSet<Property> Properties { get; set; } = null!;
  public DbSet<Image> Images { get; set; } = null!;
  public DbSet<PropType> PropTypes { get; set; } = null!;
  public DbSet<Sale> Sales { get; set; } = null!;
  public DbSet<Upgrade> Upgrades { get; set; } = null!;
  public DbSet<Favorite> Favorites { get; set; } = null!;

  public DbSet<PropertyUpgrade> PropertyUpgrades { get; set; } = null!;
  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
      switch (entry.State) {
        case EntityState.Added:
          entry.Entity.CreatedAt = DateTime.Now;
          break;
        case EntityState.Modified:
          entry.Entity.LastModifiedAt = DateTime.Now;
          break;
      }
    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    #region  Tables
    modelBuilder.Entity<Property>().ToTable("Properties");
    modelBuilder.Entity<Image>().ToTable("Images");
    modelBuilder.Entity<PropType>().ToTable("PropTypes");
    modelBuilder.Entity<Sale>().ToTable("Sales");
    modelBuilder.Entity<Upgrade>().ToTable("Upgrades");
    modelBuilder.Entity<PropertyUpgrade>().ToTable("PropertyUpgrades");
    modelBuilder.Entity<Favorite>().ToTable("Favorites");
    #endregion

    #region  Keys
    modelBuilder.Entity<Property>().HasKey(p => p.Id);
    modelBuilder.Entity<Image>().HasKey(i => i.Id);
    modelBuilder.Entity<PropType>().HasKey(t => t.Id);
    modelBuilder.Entity<Sale>().HasKey(s => s.Id);
    modelBuilder.Entity<Upgrade>().HasKey(u => u.Id);
    modelBuilder.Entity<Favorite>().HasKey(u => u.Id);

    #endregion

    #region  Relations
    modelBuilder.Entity<Property>()
      .HasMany(p => p.Images)
      .WithOne(i => i.Property)
      .HasForeignKey(i => i.PropertyId);

    modelBuilder.Entity<Property>()
      .HasOne(p => p.Type)
      .WithMany(t => t.Properties)
      .HasForeignKey(p => p.TypeId);

    modelBuilder.Entity<Property>()
      .HasOne(p => p.Sale)
      .WithMany(s => s.Properties)
      .HasForeignKey(p => p.SaleId);

    modelBuilder.Entity<Property>()
      .HasMany(p => p.PropertyUpgrades)
      .WithOne(pu => pu.Property)
      .HasForeignKey(pu => pu.PropertyId);

    modelBuilder.Entity<Upgrade>()
      .HasMany(u => u.PropertyUpgrades)
      .WithOne(pu => pu.Upgrade)
      .HasForeignKey(pu => pu.UpgradeId);

    modelBuilder.Entity<Property>()
      .HasMany(p => p.Favorites)
      .WithOne(f => f.Property)
      .HasForeignKey(f => f.PropertyId);

    #endregion


  }
}