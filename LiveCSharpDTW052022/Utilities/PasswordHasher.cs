using System;
using System.Security.Cryptography;
using System.Text;

namespace Mercadona.Utilities
{
    public class PasswordHasher
    {
        public string HashPassword(string password, out string salt)
        {
            byte[] saltBytes = GenerateSalt();
            salt = Convert.ToBase64String(saltBytes);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
            Array.Copy(saltBytes, combinedBytes, saltBytes.Length);
            Array.Copy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
