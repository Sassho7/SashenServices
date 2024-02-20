using System.ComponentModel.DataAnnotations;

namespace SmartGarage.DTOs
{
    public class UserUpdateDTO
    {
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters.")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

    }
}
