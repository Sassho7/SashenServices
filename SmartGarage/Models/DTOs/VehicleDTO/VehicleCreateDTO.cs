using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.VehicleDTO;

public class VehicleCreateDTO
{
    public string? CarMake { get; set; }

    public string? CarModel { get; set; }

    public int? CarYear { get; set; }

    public string? CarVin { get; set; }

    public string? CarLicencePlate { get; set; }

    public int CarSystemId {  get; set; }

    public User user { get; set; }

}