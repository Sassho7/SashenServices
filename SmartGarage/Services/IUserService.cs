using System.Threading.Tasks;

namespace SmartGarage.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(string email, string password, string phoneNumber);
        Task<string> LoginAsync(string email, string password);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }
}
