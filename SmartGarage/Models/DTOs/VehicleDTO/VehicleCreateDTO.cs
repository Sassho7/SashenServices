using System.ComponentModel.DataAnnotations;

namespace SmartGarage.DTOs
{
    public class VehicleCreateDTO
    {
        [Required(ErrorMessage = "Car make is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Car make must be between 2 and 20 characters.")]
        public string CarMake { get; set; }

        [Required(ErrorMessage = "Car model is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Car model must be between 2 and 20 characters.")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Car year is required.")]
        [Range(1900, 2100, ErrorMessage = "Car year must be between 1900 and 2100.")]
        public int CarYear { get; set; }

        [Required(ErrorMessage = "Car VIN is required.")]
        [StringLength(19, MinimumLength = 16, ErrorMessage = "Car VIN must be between 16 and 19 characters.")]
        public string CarVin { get; set; }

        [Required(ErrorMessage = "Car licence plate is required.")]
        [StringLength(8, MinimumLength = 7, ErrorMessage = "Car licence plate must be between 7 and 8 characters.")]
        public string CarLicencePlate { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
