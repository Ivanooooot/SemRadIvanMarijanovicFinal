using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemRadIvanMarijanovic.Models
{
    public class RentalStatus
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Rental")]
        public int RentalId { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
