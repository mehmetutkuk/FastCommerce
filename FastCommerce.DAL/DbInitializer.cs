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
            context.SaveChanges();
        }
    }
}
