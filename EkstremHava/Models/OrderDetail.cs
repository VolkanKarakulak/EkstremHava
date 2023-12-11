using System.ComponentModel.DataAnnotations;

namespace EkstremHava.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }


        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }


        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }




    }
}
