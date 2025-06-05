using FastTechFoods.ProductsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastTechFoods.ProductsService.Infrastructure.Persistence
{
    public class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

         public DbSet<Product> Products { get; set; }
    }
}
