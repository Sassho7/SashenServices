using System.ComponentModel.DataAnnotations;

namespace SmartGarage.ViewModels
{
    public class SendEmailViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
