using System.ComponentModel.DataAnnotations;

namespace EkstremHava.Models
{
    public class BasketInfo
    {

        [Key]
        public int BasketInfoId { get; set; }

        public required string Username { get; set; }

        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}