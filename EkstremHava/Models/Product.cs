#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EkstremHava.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage ="Ürün adı boş bırakılamaz")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Fiyat")]
        [Range(50, 2500, ErrorMessage ="Ürün fiyatı 50 ile 2500 arasında olmalıdır")]
        public decimal Price { get; set; }

        [DisplayName("Görsel")]
        [StringLength(180)]
        public required string ImageFileName { get; set; }

        [DisplayName("Açıklama:")]
        [StringLength(750)]
        [Required(ErrorMessage = "Ürün açıklaması boş bırakılamaz")]
        public required string Description { get; set; }

        [DisplayName("Tür")]
        public int ProductTypeId { get; set; }

        [DisplayName("Tür:")]
        public virtual ProductType Type { get; set; }

        [DisplayName("Favorilerim")]
        public bool IsFavorite { get; set; }


        //public bool FavoriMi {  get; set; }
        

    }
}
