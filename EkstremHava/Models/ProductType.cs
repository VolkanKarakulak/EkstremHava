using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace EkstremHava.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [DisplayName("Ürün Türü Adı")]
        [Required(ErrorMessage ="Ürün türüne verilecek ad boş bırakılamaz")]
        public required string ProductTypeName { get; set; }

        [DisplayName("Açıklama")]
        [StringLength(750)]
        [Required(ErrorMessage = "Ürün türü açıklaması boş bırakılamaz")]
        public required string Description { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}
