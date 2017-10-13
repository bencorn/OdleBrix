using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BUDLP.Models.AuthUserModels
{
    public class AuthUserProfile
    {
        public int AuthUserProfileId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int YearOfBirth { get; set; }
        public string LevelOfEducation { get; set; }
        public string Goals { get; set; }
        [Required]
        public string Country { get; set; }
        public string City { get; set; }
        public string Bio { get; set; }
    }
}
