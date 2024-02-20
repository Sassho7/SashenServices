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

    public Vehicle(string? carMake, string? carModel, string? carVin, int? carYear, string? carLicencePlate, int carSystemId)
    {
        CarMake = carMake;
        CarModel = carModel;
        CarVin = carVin;
        CarLicencePlate = carLicencePlate;
        Id = carSystemId;
    }

    [Key]
    public int Id { get; set; }

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


    public User User { get; set; }

    [ForeignKey("User")]
    public int userId { get; set; }

    public bool IsDeleted { get; internal set; }
}