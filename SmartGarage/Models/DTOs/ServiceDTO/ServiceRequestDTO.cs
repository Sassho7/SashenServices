using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.ServiceDTO
{
    public class ServiceRequestDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

    }
}
