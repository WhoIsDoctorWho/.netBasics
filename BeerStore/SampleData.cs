using BeerStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore
{
    public class SampleData
    {
        public static void Initialize(BeerStoreContext context)
        {
            bool isBeers = context.Beers.Any();
            bool isUsers = context.Users.Any();
            bool anyChanges = false;
            if (!isBeers) {
                GenerateBeers(context);
                anyChanges = true;
            }
            if(!isUsers)
            {
                GenerateUsers(context);
                anyChanges = true;
            }
            if (anyChanges)
                context.SaveChanges();
            
        }
        private static void GenerateBeers(BeerStoreContext context)
        {
            context.Beers.AddRange(
                    new Beer
                    {
                        Name = "Obolonske",
                        Description = "Strong",
                        ImageUrl = "img",
                        Price = 12
                    },
                    new Beer
                    {
                        Name = "Cool lager",
                        Description = "Light",
                        ImageUrl = "img",
                        Price = 15
                    },
                    new Beer
                    {
                        Name = "Beer mix",
                        Description = "Lemon",
                        ImageUrl = "img",
                        Price = 18
                    }
                );
        }
        private static void GenerateUsers(BeerStoreContext context)
        {
            context.Users.AddRange(
                    new User
                    {
                        Fullname = "Dima Timcnkko",                        
                        AvaUrl = "img",
                        Registered = DateTime.Now,
                        Login = "timchnk",
                        Password = "Cucumber2000"                        
                    },
                    new User
                    {
                        Fullname = "Tanya Protas",
                        AvaUrl = "img",
                        Registered = DateTime.Now,
                        Login = "TB",
                        Password = "zhopa"
                    },
                    new User
                    {
                        Fullname = "Andrey Goncharenko",
                        AvaUrl = "img",
                        Registered = DateTime.Now,
                        Login = "Goncharik",
                        Password = "whatisgivence"
                    }
                );
        }

    }
}
