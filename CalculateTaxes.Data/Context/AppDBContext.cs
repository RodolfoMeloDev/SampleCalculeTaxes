using CalculateTaxes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Context
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<FeatureFlagEntity> FeatureFlags { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ItemsOrderEntity> ItemsOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FeatureFlagEntity>().HasIndex(i => i.Name, "IDX_FeatureFlag_Name");
        }

    }
}