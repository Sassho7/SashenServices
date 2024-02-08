using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.VehicleDTO;

public class VehicleCreateDTO
{
    [MinLength(2), MaxLength(20)]
    public string? CarMake { get; set; } // marka

    [MinLength(2), MaxLength(20)]
    public string? CarModel { get; set; } // model

    [MinLength(4), MaxLength(4)]
    public int? CarYear { get; set; } // godina

    [MinLength(16), MaxLength(19)]
    public string? CarVin { get; set; } // vin nomer

    [MinLength(7), MaxLength(8)]
    public string? CarLicencePlate { get; set; } // registracionen nomer

    public User user { get; set; }

}