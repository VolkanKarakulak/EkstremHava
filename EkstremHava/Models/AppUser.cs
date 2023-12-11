#nullable disable

using Microsoft.AspNetCore.Identity;


namespace EkstremHava.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public virtual List<Address> Addresses { get; set; }

        public virtual List<Product> FavoriteProducts { get; set; }

        public virtual List<Order> Orders { get; set; }

        //public virtual Address Address { get; set; } 

        public AppUser()
        {
            EmailConfirmed = true;
        }


    }   
}
