using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.ServiceDTO
{
    public class UpdateServiceResponseDTO
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int VehicleId { get; set; }
    }
}
