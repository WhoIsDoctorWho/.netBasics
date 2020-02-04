using System;

namespace BeerStore.Models
{
    public class User
    {
		public int id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Fullname{ get; set; }
		public DateTime Registered { get; set; }
		//public bool role { get; set; }
		public string AvaUrl{ get; set; }
		//public Order Order { get; set; }

    }
}
