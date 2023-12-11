
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EkstremHava.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [DisplayName("Sokak")]
        public required string Street { get; set; }

        [DisplayName("Şehir")]
        public required string City { get; set; }

        [DisplayName("Ülke")]
        public required string Country { get; set; }

        public AppUser? AppUser { get; set; }

        //public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
