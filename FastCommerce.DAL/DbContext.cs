using System.Collections.Generic;
using FastCommerce.Entities.Entities;
using Microsoft.EntityFrameworkCore;


namespace FastCommerce.DAL
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Property> Propertys { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleObject> RoleObjects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=fastCommerce;Username=postgres;Password=123");


        public ProductContext(DbContextOptions options) : base(options)
        {
        }
    }


}