using System.ComponentModel.DataAnnotations;

namespace SemRadIvanMarijanovic.Models
{
    public class User
    {
        public enum UserType
        {
            Admin,
            Customer
        }

        [Key]
        public int Id { get; set; }

        public UserType Type { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string? Password { get; set; }
        
        public string ConfirmationPassword { get; set; }

        [Required]
        public bool IsEmailConfirmed { get; set; }
    }
}
