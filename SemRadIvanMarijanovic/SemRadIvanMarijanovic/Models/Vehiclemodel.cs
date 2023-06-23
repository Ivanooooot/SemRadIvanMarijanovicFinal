using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SemRadIvanMarijanovic.Models
{
    public class Vehiclemodel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string? Image { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
