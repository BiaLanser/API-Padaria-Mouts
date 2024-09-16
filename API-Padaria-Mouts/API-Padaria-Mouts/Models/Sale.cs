using System.ComponentModel.DataAnnotations.Schema;

namespace API_Padaria_Mouts.Models
{
    public class Sale
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal FinalPrice { get; set; }
        public int Points { get; set; }
    }
}
