using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EkstremHava.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        //public string? UserId { get; set; }
        public  required string Username { get; set; }

        public AppUser? AppUser { get; set; }

        public required string Name { get; set; }
        public required string Surname { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string? DeliveryAddress { get; set; }
        public string? City { get; set; }

        public string? PhoneNumber { get; set; }



        //public int AddressId { get; set; }
        //public required Address Address { get; set; }

        public virtual List<OrderDetail>? OrderDetails { get; set; }


    }
}
