using System.ComponentModel.DataAnnotations.Schema;

namespace API_Padaria_Mouts.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }

    }
}
