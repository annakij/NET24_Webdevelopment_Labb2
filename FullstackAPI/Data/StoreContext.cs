using FullstackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FullstackAPI.Data;

public class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProducts> OrderProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProducts>()
            .HasOne(op => op.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProducts>()
            .HasOne(op => op.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        base.OnModelCreating(modelBuilder);
    }
}
