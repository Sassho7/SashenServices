using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using SmartGarage.Models.Enums;

namespace SmartGarage.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 20 characters.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$", ErrorMessage = "Password must meet the criteria.")]
    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
    public string PhoneNumber { get; set; }

    public User(string username, string passwordHash, string email, string phoneNumber)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
