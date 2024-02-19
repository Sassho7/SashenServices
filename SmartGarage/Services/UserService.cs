using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartGarage.Models;

namespace SmartGarage.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
    }
}
