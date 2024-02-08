using System.ComponentModel.DataAnnotations;
using SmartGarage.Models.Enums;

namespace SmartGarage.Models;

public class User
{
    public User(string firstName, string lastName, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }

    [MinLength(2), MaxLength(20)]
    public string FirstName { get; set; } // first name

    [MinLength(2), MaxLength(20)]
    public string LastName { get; set; } // last name

    public int Id { get; set; }

    [MinLength(2), MaxLength(20)]
    public string UserName { get; set; } // user name

    [MinLength(6), MaxLength(20)]
    public string Password { get; set; } // password

    [EmailAddress, MaxLength(100)]
    public string EmailAddress { get; set; } // email address

    [MinLength(10), MaxLength(13)]
    public int PhoneNumber { get; set; } // phone number

    public UserRole Role { get; set; }

    public UserStatus Status { get; set; }

    //list ot takskove
    //list ot nqkakwo ime za svurshena rabota

}