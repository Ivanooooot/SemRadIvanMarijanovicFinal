using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static SemRadIvanMarijanovic.Models.User;

namespace SemRadIvanMarijanovic.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    public UserType Type { get; set; }


    public bool IsEmailConfirmed { get; set; }
}

