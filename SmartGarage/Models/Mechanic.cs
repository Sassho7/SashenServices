using System.ComponentModel.DataAnnotations;
using SmartGarage.Models.Enums;

namespace SmartGarage.Models;

public class Mechanic
{
    public Mechanic(string firstName, int phoneNumber, UserStatus status)
    {
        FirstName = firstName;
        PhoneNumber = phoneNumber;
        Status = status;
    }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string LastName { get; set; }

    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string EmailAddress { get; set; }

    [Required]
    [MinLength(6), MaxLength(20)]
    public string Password { get; set; }

    [Required]
    [MinLength(10), MaxLength(13)]
    public int PhoneNumber { get; set; }

    public UserStatus Status {  get; set; }

    //public Vehicle VehicleId { get; set; }


}