using Microsoft.AspNetCore.Identity;
using System;

namespace SmartGarage.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
