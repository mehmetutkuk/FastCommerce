using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Cryptography;

namespace FastCommerce.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(dbContext context)
        {
            context.Database.EnsureCreated();

            var users = new User[]
            {
                new User{
                  Email= "Arturo.Anand@gmail.com",
                  Name= "Arturo",
                  Surname= "Anand",
                  ProfilePhoto= "",
                  Password= Cryptography.Encrypt("Arturo123"),
                  PhoneNumber= "5300615200",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },
                new User{
                  Email= "Meredith.Alonso@gmail.com",
                  Name= "Meredith",
                  Surname= "Alonso",
                  ProfilePhoto= "",
                  Password= Cryptography.Encrypt("Meredith123"),
                  PhoneNumber= "5300615201",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },
                new User{
                  Email= "Gytis.Barzdukas@gmail.com",
                  Name= "Gytis",
                  Surname= "Barzdukas",
                  ProfilePhoto= "",
                  Password= Cryptography.Encrypt("Gytis123"),
                  PhoneNumber= "5300615202",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },
                new User{
                  Email= "Yan.Li@gmail.com",
                  Name= "Yan",
                  Surname= "Li",
                  ProfilePhoto= "",
                  Password=  Cryptography.Encrypt("Yan123"),
                  PhoneNumber= "5300615203",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },

                new User{
                  Email= "Peggy.Justice@gmail.com",
                  Name= "Peggy",
                  Surname= "Justice",
                  ProfilePhoto= "",
                  Password=  Cryptography.Encrypt("Peggy123"),
                  PhoneNumber= "5300615204",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },
                new User{
                  Email= "Mike.Pence@gmail.com",
                  Name= "Mike",
                  Surname= "Pence",
                  ProfilePhoto= "",
                  Password= Cryptography.Encrypt("Mike123"),
                  PhoneNumber= "5300615200",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                },
                new User{
                  Email= "mehmetburakeker@gmail.com",
                  Name= "Burak",
                  Surname= "Eker",
                  ProfilePhoto= "",
                  Password= Cryptography.Encrypt("test26"),
                  PhoneNumber= "5300615200",
                  RegisterDate= DateTime.UtcNow,
                  LastLoginDate= DateTime.UtcNow,
                  Active= true
                }
            };

            var properties = new Property[]
            {
                new Property
                {
                    PropertyName ="Genişlik",
                    PropertyValue = "1-4"
                },
                 new Property
                {
                    PropertyName ="Renk",
                    PropertyValue = "Beyaz"
                }

            };

            var categories = new Category[]
            {
                new Category
                {

                    CategoryName ="Altın Yüzükler",
                    Properties= properties.ToList()

                }

            };

            if (!context.Properties.Any())
            {
                foreach (Property p in properties)
                {
                    context.Properties.Add(p);
                }
            }
            var products = new Product[]
            {
                new Product{
                  ProductId=1,
                  ProductName="Golden Ring w/ Topaz 22k",
                  LastModified=DateTime.UtcNow,
                  Rating=3,
                  Price=350.5,
                  ProductCategories=new List<ProductCategories>(){ new ProductCategories() {CategoryId= 1,ProductId = 1}, new ProductCategories() { CategoryId = 2, ProductId = 1 } }
                },
                new Product{
                  ProductId=2,
                  ProductName="Golden Ring w/ Diamond 24k",
                  LastModified=DateTime.UtcNow,
                  Rating=3,
                  Price=750.5,
                  ProductCategories=new List<ProductCategories>(){ new ProductCategories() {CategoryId= 1,ProductId = 2}, new ProductCategories() { CategoryId = 2, ProductId = 2 } }
                },
                new Product{
                  ProductId=3,
                  ProductName="Golden Ring w/ Ruby 22k",
                  LastModified=DateTime.UtcNow,
                  Rating=4,
                  Price=550.5,
                  ProductCategories=new List<ProductCategories>(){ new ProductCategories() {CategoryId= 1,ProductId = 3}, new ProductCategories() { CategoryId = 2, ProductId = 3 } }
                },
                new Product{
                  ProductId=4,
                  ProductName="Silver Ring w/ Emerald 22k",
                  LastModified=DateTime.UtcNow,
                  Rating=4,
                  Price=200,
                  ProductCategories=new List<ProductCategories>(){ new ProductCategories() {CategoryId= 1,ProductId = 4}, new ProductCategories() { CategoryId = 2, ProductId = 4 } }
                },
                new Product{
                  ProductId=5,
                  ProductName="Silver Ring w/ Amethyst 22k",
                  LastModified=DateTime.UtcNow,
                  Rating=2,
                  Price=250,
                  ProductCategories=new List<ProductCategories>(){ new ProductCategories() {CategoryId= 1,ProductId = 4}, new ProductCategories() { CategoryId = 2, ProductId = 4 } }
                },
            };

            var StockProperties = new StockProperties[]
            {
                new StockProperties
                {
                    PropertyID = 2,
                    StockId = 1,
                },
                 new StockProperties
                {
                    PropertyID = 1,
                    StockId = 1,
                }
            };
            var stocks = new Stock[] {
                new Stock
                {
                    ProductId =5,
                    Quantity = 1,
                }
            };
         
            if (!context.Stocks.Any())
            {
                foreach (Stock s in stocks)
                {
                    context.Stocks.Add(s);
                }
            }

            if (!context.Category.Any())
            {
                foreach (Category c in categories)
                {
                    context.Category.Add(c);
                }
            }

            if (!context.Products.Any())
            {
                foreach (Product p in products)
                {
                    context.Products.Add(p);
                }
            }

            if (!context.Users.Any())
            {
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
            }
            context.SaveChanges();
            if (!context.StockProperties.Any())
            {
                foreach (StockProperties sp in StockProperties)
                {
                    context.StockProperties.Add(sp);
                }
            }


            context.SaveChanges();
        }
    }
}
