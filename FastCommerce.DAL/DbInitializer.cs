using FastCommerce.Entities.Entities;
using GenFu;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Utility.Cryptography;

namespace FastCommerce.DAL
{
    public static class DbInitializer
    {
        private static List<T> FillAllProperties<T>(int count) where T : new()
        {
            var results = A.ListOf<T>(count);
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo PKInfo = null;
            PropertyInfo FKInfo;
            int Pk = 0;
            foreach (var prop in properties)
            {
                List<Attribute> attrs = prop.GetCustomAttributes().ToList();
                foreach (var obj in attrs)
                {
                    if (obj.GetType() == typeof(ForeignKey))
                    {
                        FKInfo = prop;
                        break;
                    }
                    if (obj.GetType() == typeof(KeyAttribute))
                    {
                        PKInfo = prop;
                        break;
                    }
                }
            }

            if (PKInfo != null)
            {
                foreach (T row in results)
                {

                    row.GetType().GetProperty(PKInfo.Name).SetValue(row, 0);
                }
            }
            return results.ToList();
        }

        public static double GenerateDouble(int maxValue) =>
            Math.Round((new Random().NextDouble() * maxValue), 2, MidpointRounding.AwayFromZero);
        public async static void Initialize(dbContext context)
        {

            context.Database.EnsureCreated();
            if (context.Products.Any())
                return;

            int count = 5;
            int countSquare = (int) Math.Pow(count, 2);
            A.Configure<Product>()
                .Fill(p => p.Price, GenerateDouble(500))
                .Fill(p => p.Discount, GenerateDouble(40))
                .Fill(p => p.Rating, GenerateDouble(5))
                .Fill(p => p.ViewCount, () => new Random().Next(250));
            List<Product> products = new List<Product>();
            products.Add(new Product() { ProductName = "Model Y Yüzük"});

            List<Category> categories = new List<Category>();
            categories.Add(new Category() { CategoryName = "Trending Products" });
            categories.Add(new Category() { CategoryName = "Rings" });
            categories.Add(new Category() { CategoryName = "Necklaces" });
            categories.Add(new Category() { CategoryName = "Bracelets" });

            List<ProductCategories> productCategories = new List<ProductCategories>();

            productCategories.Add(new ProductCategories() { Product = products[0], Category = categories[1] });

            List<Address> addresses = FillAllProperties<Address>(count);

            List<Entities.Entities.Property> properties = new List<Entities.Entities.Property>();

            properties.Add(new Entities.Entities.Property() {  PropertyName = "Renk",PropertyType = Entities.Models.Enums.PropertyType.String, Category = categories[1]  });
            properties.Add(new Entities.Entities.Property() { PropertyName = "Geniþlik", PropertyType = Entities.Models.Enums.PropertyType.String, Category = categories[1] });

            List<PropertyDetail> propertyDetails = new List<PropertyDetail>();
            propertyDetails.Add(new PropertyDetail() { PropertyValue = "100", Property = properties[1] });
            propertyDetails.Add(new PropertyDetail() { PropertyValue = "200", Property = properties[1] });
            propertyDetails.Add(new PropertyDetail() { PropertyValue = "300", Property = properties[1] });
            propertyDetails.Add(new PropertyDetail() { PropertyValue = "Sarý", Property = properties[0] });
            propertyDetails.Add(new PropertyDetail() { PropertyValue = "Beyaz", Property = properties[0] });


       
            foreach (var item in addresses)
            {
                item.UserId = 1;

            }
          

   

            List<User> users = FillAllProperties<User>(count);

            users.Add(new User(){Email="mehmetburakeker@gmail.com",Password="Burak26",Active = true});
            foreach (var user in users)
            {
                user.Password = Cryptography.Encrypt(user.Password);
            }

            await context.AddRangeAsync(users);
            
            await context.AddRangeAsync(productCategories);
            await context.AddRangeAsync(products);
            await context.AddRangeAsync(properties);
            await context.AddRangeAsync(propertyDetails);
            await context.AddRangeAsync(categories);
            context.SaveChanges();
            await context.AddRangeAsync(addresses);
            context.SaveChanges();
        }
    }
}
