﻿using System.ComponentModel.DataAnnotations;

namespace SmartGarage.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[+*^$%&!@#_-])[A-Za-z\d+*^$%&!@#_-]{8,}$")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[+*^$%&!@#_-])[A-Za-z\d+*^$%&!@#_-]{8,}$")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
