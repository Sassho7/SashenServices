using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.ServiceDTO
{
    public class ServiceResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
