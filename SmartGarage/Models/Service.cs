using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace SmartGarage.Models

{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool isDeleted { get; set; }
    }
}
