using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SemRadIvanMarijanovic.Models
{
    public class VehiclemodelCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Required]
        public int VehiclemodelId { get; set; }

        [ForeignKey("VehiclemodelId")]
        public Vehiclemodel? Vehiclemodel { get; set; }
    }
}
