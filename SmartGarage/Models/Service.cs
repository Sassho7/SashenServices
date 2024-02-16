namespace SmartGarage.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
