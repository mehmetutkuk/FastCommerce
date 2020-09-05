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

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

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

            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            var products = new Product[]
            {
                new Product{
                  ProductId=1,
                  ProductName="Golden Ring w/ Topaz 22k",
                  LastModified=DateTime.UtcNow,
                  Quantity=6,
                  Rating=3,
                  Price=350.5,
                  Categories=new List<Category>(){ new Category() {CategoryID= 1,CategoryName="Top Section" }}
                },
                new Product{
                  ProductId=2,
                  ProductName="Golden Ring w/ Diamond 24k",
                  LastModified=DateTime.UtcNow,
                  Quantity=3,
                  Rating=3,
                  Price=750.5,
                  Categories=new List<Category>(){}
                },
                new Product{
                  ProductId=3,
                  ProductName="Golden Ring w/ Ruby 22k",
                  LastModified=DateTime.UtcNow,
                  Quantity=20,
                  Rating=4,
                  Price=550.5,
                  Categories=new List<Category>(){}
                },
                new Product{
                  ProductId=4,
                  ProductName="Silver Ring w/ Emerald 22k",
                  LastModified=DateTime.UtcNow,
                  Quantity=15,
                  Rating=4,
                  Price=200,
                  Categories=new List<Category>(){}
                },
                new Product{
                  ProductId=5,
                  ProductName="Silver Ring w/ Amethyst 22k",
                  LastModified=DateTime.UtcNow,
                  Quantity=33,
                  Rating=2,
                  Price=250,
                  Categories=new List<Category>(){}
                },
            };
            foreach (Product s in products)
            {
                context.Products.Add(s);
            }
            context.SaveChanges();
        }
    }
}
