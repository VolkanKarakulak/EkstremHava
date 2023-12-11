using System.ComponentModel.DataAnnotations;

namespace EkstremHava.Models
{
    public class FavoriteProduct
    {
        [Key]
        public int FavoriteProductId { get; set; }

        public required string Username { get; set; }  //KullaniciId de kullanılabilir

        public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
