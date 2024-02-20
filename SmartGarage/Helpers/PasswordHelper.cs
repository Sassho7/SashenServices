using System;
using System.Security.Cryptography;
using System.Text;

namespace SmartGarage.Helpers
{
    public interface IPasswordHelper
    {
        string GenerateRandomPassword();
        string HashPassword(string password);
    }

    public class PasswordHelper : IPasswordHelper
    {
        private const int DefaultPasswordLength = 10;

        public string GenerateRandomPassword()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=-";

            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < DefaultPasswordLength; i++)
            {
                int index = random.Next(validChars.Length);
                sb.Append(validChars[index]);
            }

            return sb.ToString();
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
