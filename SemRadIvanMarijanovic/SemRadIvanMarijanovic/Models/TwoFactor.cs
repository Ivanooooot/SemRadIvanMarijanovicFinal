using System.ComponentModel.DataAnnotations;
namespace SemRadIvanMarijanovic.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
