using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string PhoneNumber { get; set; }
    }
}
