using SmartGarage.Models.DTOs.UserDTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartGarage.Models.DTOs.VehicleDTO
{
    public class VehicleResponseDTO
    {
            public string LicensePlate { get; set; }
            public string VIN { get; set; }
            public int Year { get; set; }
            public string Model { get; set; }
            public string Brand { get; set; }
            public UserResponseDTO User { get; set; }
       //     public List<Visit> Visits { get; set; }
    }
}
