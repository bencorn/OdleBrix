using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BUDLP.Models
{
    // Student User Account Basic Profile Settings
    public class AuthenticatedUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public bool IsStaff { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int TargetLanguage { get; set; }
        public bool IsSuperUser { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime? DateJoined { get; set; }
    }
}
