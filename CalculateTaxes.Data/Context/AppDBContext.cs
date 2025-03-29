using CalculateTaxes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculateTaxes.Data.Context
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }

    }
}