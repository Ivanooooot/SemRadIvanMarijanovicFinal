using Microsoft.AspNetCore.Identity;
using SemRadIvanMarijanovic.Areas.Identity.Data;

namespace SemRadIvanMarijanovic.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}

