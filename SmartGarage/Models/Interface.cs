using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;

namespace Forum.Models;

public class User
{
    public User(string username, string password, string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }

    public User()
    {
    }

    public int Id { get; init; }

 

    [EmailAddress, MaxLength(254)]
    public string Email { get; set; }

    [MinLength(2), MaxLength(20)]
    public string Username { get; init; }

    [MinLength(8), MaxLength(64)]
    public string Password { get; set; }

    [MinLength(4), MaxLength(32)]
    public string? PhoneNumber { get; set; }

}