using FastCommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            new User{Name="Carson",Surname="Alexander"},

            new User{Name="Meredith",Surname="Alonso"},

            new User{Name="Arturo",Surname="Anand"},

            new User{Name="Gytis",Surname="Barzdukas"},

            new User{Name="Yan",Surname="Li"},

            new User{Name="Peggy",Surname="Justice"},

            new User{Name="Laura",Surname="Norman"},

            new User{Name="Nino",Surname="Olivetto"},
            };

            foreach (User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();
        }
    }
}
