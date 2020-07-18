using System;
using System.Collections.Generic;
using FastCommerce.Entities.Entities;
using Microsoft.EntityFrameworkCore;
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

   

}