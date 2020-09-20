using System;
using System.Collections.Generic;
using System.IO;
using FastCommerce.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastCommerce.DAL
{
    public class dbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockProperties> StockProperties { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleObject> RoleObjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        public dbContext(DbContextOptions options) : base(options)
        {

        }

    }


    public class DesignTimeDbContextFactory :  IDesignTimeDbContextFactory<dbContext>
    {
        public dbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<dbContext> builder = new DbContextOptionsBuilder<dbContext>();
            //string connectionString = Environment.GetEnvironmentVariable("dbConnectionString");
            var connectionString = "host=postgres_image;port=5432;Database=fastCommerce;Username=postgres;Password=postgresPassword;";
            builder.UseNpgsql(connectionString);
            Console.WriteLine($"Running DesignTime DB context. ({connectionString})");
            return new dbContext(builder.Options);
        }
    }

}