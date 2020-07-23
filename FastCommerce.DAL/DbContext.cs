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
        public DbSet<Property> Propertys { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleObject> RoleObjects { get; set; }
        public DbSet<User> Users { get; set; }


        public dbContext(DbContextOptions options) : base(options)
        {

        }

    }


    public class DesignTimeDbContextFactory :  IDesignTimeDbContextFactory<dbContext>
    {
        public dbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<dbContext> builder = new DbContextOptionsBuilder<dbContext>();
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            //var connectionString = "host=postgres_image;port=5432;Database=fastCommerce;Username=postgres;Password=postgresPassword;";
            builder.UseNpgsql(connectionString);
            Console.WriteLine($"Running DesignTime DB context. ({connectionString})");
            return new dbContext(builder.Options);
        }
    }

}