using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartGarage.Models;

public class Vehicle
{
    public Vehicle() { }

    public Vehicle(string carMake, string carModel, string carVin, int id) 
    { 
        CarMake = carMake;
        CarModel = carModel;
        CarVin = carVin;
        Id = id;
    }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string CarMake { get; set; } // marka

    [Required]
    [MinLength(2), MaxLength(20)]
    public string CarModel { get; set; } // model

    [Required]
    [MinLength(4),  MaxLength(4)]
    public int CarYear { get; set; } // godina

    [Required]
    [MinLength(16), MaxLength(19)]
    public string CarVin {  get; set; } // vin nomer

    [Required]
    [MinLength(7),  MaxLength(8)]
    public string CarLicencePlate { get; set; } // registracionen nomer

    public int CarSystemId { get; set; } // unikalen id nomer v sistemata

    [ForeignKey("User")]
    public int Id { get; set; }

    public string MechanicToVehicle { get; set; }

}