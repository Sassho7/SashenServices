using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartGarage.Models;

namespace SmartGarage.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<bool> RegisterAsync(string email, string password, string phoneNumber)
        {
            var user = new ApplicationUser { UserName = email, Email = email, PhoneNumber = phoneNumber };
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return "Login successful";
            }
            return null;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, currentPassword))
            {
                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> UpdateProfileAsync(string email, string phoneNumber)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.PhoneNumber = phoneNumber;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            return false;
        }
        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
       
                var resetPasswordLink = $"https://yourdomain.com/resetpassword?email={email}&token={token}";

                // Send reset password link with the token to the user's email
                _emailService.SendEmail(email, "Password Reset", $"Click the link to reset your password: {resetPasswordLink}");
                return true;
            }
            return false;
        }
    }
}
