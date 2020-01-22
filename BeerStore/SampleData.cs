using BeerStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore
{
    public class SampleData
    {
        public static void Initialize(BeerContext context)
        {
            if(!context.Beers.Any())
            {
                context.Beers.AddRange(
                    new Beer
                    {
                        Name = "Obolonske",
                        Description = "Strong",
                        ImageURL = "img",
                        Price = 12
                    },
                    new Beer
                    {
                        Name = "Cool lager",
                        Description = "Light",
                        ImageURL = "img",
                        Price = 15
                    },
                    new Beer
                    {
                        Name = "Beer mix",
                        Description = "Lemon",
                        ImageURL = "img",
                        Price = 18
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
