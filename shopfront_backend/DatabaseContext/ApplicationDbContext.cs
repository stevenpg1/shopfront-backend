using Microsoft.EntityFrameworkCore;
using shopfront_backend.Models;

namespace shopfront_backend.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("9F1D81E9-1A19-441B-8CF1-127C819836C5"),
                Name = "Hammer",
                StockCount = 100,
                UnitPrice = 12.99m
            });

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("2A327E00-933A-4440-AEBF-C6FA83895E1D"),
                Name = "Spanner",
                StockCount = 500,
                UnitPrice = 7.99m
            });

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("B70D0BB7-AE17-4B96-B241-1E10C38AD8EB"),
                Name = "Wrench",
                StockCount = 40,
                UnitPrice = 21.99m
            });

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("8BA5622A-FB02-4B90-8A9F-7B2B89BA394F"),
                Name = "Screwdriver Set",
                StockCount = 45,
                UnitPrice = 11.99m
            });

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("3A014BCB-77F0-4FC4-90B1-6FEC398A6D3D"),
                Name = "Toolbox",
                StockCount = 10,
                UnitPrice = 45.99m
            });

            modelBuilder.Entity<StockItem>().HasData(new StockItem
            {
                Id = Guid.Parse("FDC7EF07-512B-46B7-A9FD-BF1022B3806A"),
                Name = "Box of 50 2inch Nails",
                StockCount = 50,
                UnitPrice = 2.99m
            });

        }
    }
}
