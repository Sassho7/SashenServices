namespace SmartGarage.Models.DTOs.UserDTO
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<int> VehicleIds { get; set; } = new List<int>();
        public bool IsEmployee { get; internal set; }
    }
}
