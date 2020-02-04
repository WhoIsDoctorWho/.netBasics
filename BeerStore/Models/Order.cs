using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerStore.Models
{
    public class Order
    {
        public int id { get; set; }
        public string User { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public List<ShopListItem> BeerIds { get; set; }

        public List<Beer> Beer;
    }
}
