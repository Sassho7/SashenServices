using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.VehicleDTO
{
    public class VehicleRequestDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [RegularExpression(@"^[A-C, E, H, K, M, O, P, T, X, Y]{1,2}\s?\d{4}[A-C, E, H, K, M, O, P, T, X, Y]{2}$", ErrorMessage = "Please enter a valid license plate")]
        public string LicensePlate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [StringLength(17, ErrorMessage = "The {0} must be exactly {1} characters.")]
        public string VIN { get; set; }

        [Required]
        [Range(1886, 2024, ErrorMessage = "The {0} must be between {1} and {2}.")]
        public int Year { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(50, ErrorMessage = "The {0} must be less than {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} character.")]
        public string Brand { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The {0} field is required and must not be an empty string.")]
        [MaxLength(50, ErrorMessage = "The {0} must be less than {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} character.")]
        public string Model { get; set; }

       
        [Required]
        public int UserId { get; set; }
    }
}
