using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.VehicleDTO;

public class VehicleDeleteDTO
{
    public string? CarVin { get; set; }

    public string? CarLicencePlate { get; set; }

    public int? CarSystemId { get; set; }
}