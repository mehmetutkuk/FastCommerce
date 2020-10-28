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
            List<Product> products = FillAllProperties<Product>(count);

            List<Category> categories = FillAllProperties<Category>(count);

            List<ProductCategories> productCategories = FillAllProperties<ProductCategories>(countSquare);
            List<Address> addresses = FillAllProperties<Address>(count);

            List<Entities.Entities.Property> properties = FillAllProperties<Entities.Entities.Property>(5);
            categories.Add(new Category() { CategoryName = "Trending Products" });
            foreach (var item in categories)
            {
                item.Properties = properties.ToList();
            }

            for (var j = 0; j < countSquare; j++)
            {
                productCategories[j].Product = products.ToList()[j / count];
                productCategories[j].Category = categories.ToList()[j % 5];
            }

            List<StockPropertyCombination> stockPropertyCombinations = FillAllProperties<StockPropertyCombination>(5);
            List<PropertyDetail> propertyDetails = FillAllProperties<PropertyDetail>(5);
            List<Stock> stocks = FillAllProperties<Stock>(5);
            int i = 0;
            foreach (var item in stocks)
            {
                item.Product = products.ToList()[i];
                i++;

            }
            foreach (var item in addresses)
            {
                item.UserId = 1;

            }
            foreach (var item in stockProperties)
            {
                propertyDetails[pr].Property = properties[pr];
                propertyDetails[pr].StockPropertyCombination = stockPropertyCombinations[pr];
            }

            for (var pt = 0; pt < count; pt++)
            {
                stockPropertyCombinations[pt].Stock = stocks[pt];
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
            await context.AddRangeAsync(stockPropertyCombinations);
            await context.AddRangeAsync(stocks);
            context.SaveChanges();
            await context.AddRangeAsync(addresses);
            context.SaveChanges();
        }
    }
}
