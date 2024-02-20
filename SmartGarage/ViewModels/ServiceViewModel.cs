using System.ComponentModel.DataAnnotations;

namespace SmartGarage.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
