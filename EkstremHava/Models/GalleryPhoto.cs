#nullable disable 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace EkstremHava.Models

{
    public class GalleryPhoto
    {
        [Key]
        public int GalleryPhotoId { get; set; }

        [StringLength(180)]
        [DisplayName("Fotoğraf")]
        public string? PhotoName { get; set; }

        [Required(ErrorMessage ="Açıklama boş bırakılamaz")]
        [DisplayName("Açıklama")]
        public required string Description { get; set; }

        [DisplayName("Tarih")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Konum bilgisi boş bırakılamaz")]
        [DisplayName("Konum")]
        public required string LocationName { get; set; } 

    }
}
