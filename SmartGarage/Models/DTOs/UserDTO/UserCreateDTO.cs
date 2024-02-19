using SmartGarage.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartGarage.Models.DTOs.UserDTO;

public class UserCreateDto
{
    public string FriendName { get; set; }

    public string LastName { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string EmaiAddress { get; set; }

    public string PhoneNumber { get; set; }

    public UserRole Role { get; set; }

    public UserStatus Status { get; set; }
}