using System.ComponentModel.DataAnnotations;

namespace SmartGarage.DTOs
{
    public class UserQueryParametersDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
     
    }
}
