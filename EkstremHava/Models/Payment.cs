using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EkstremHava.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public string CardName { get; set; }
        public DateTime ExpirationDate { get; set; }
       
        public int CVV { get; set; }

    }
}
