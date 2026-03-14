using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeServicios.Encription
{
    public static class Hash
    {
        private const int SaltSize = 16; // 128 bits
        private const int KeySize = 32;  // 256 bits
        private const int DefaultIterations = 100_000;

        // Hashea una contrase;a de texto plano devolviendo la contrase;a

        public static string HashPassword(string password)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (password.Length == 0) throw new ArgumentException("Password cannot be empty.", nameof(password));

            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, DefaultIterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            return $"{DefaultIterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        /// Verifies a plaintext password against a stored hash in the format produced by HashPassword.
        /// Returns true if the password matches.
        
        public static bool VerifyPassword(string password, string storedHash)
        {
            if (password is null) throw new ArgumentNullException(nameof(password));
            if (storedHash is null) throw new ArgumentNullException(nameof(storedHash));

            // Expected format: iterations.salt.key
            var parts = storedHash.Split('.', 3);
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations)) return false;
            byte[] salt;
            byte[] key;
            try
            {
                salt = Convert.FromBase64String(parts[1]);
                key = Convert.FromBase64String(parts[2]);
            }
            catch (FormatException)
            {
                return false;
            }

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = pbkdf2.GetBytes(key.Length);

            // Use constant-time comparison to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(key, keyToCheck);
        }

        /// <summary>
        /// Optional helper to check if a stored hash uses fewer iterations than current default (for migration/upgrade).
        /// </summary>
        public static bool NeedsUpgrade(string storedHash)
        {
            if (string.IsNullOrWhiteSpace(storedHash)) return true;
            var parts = storedHash.Split('.', 3);
            if (parts.Length != 3) return true;
            if (!int.TryParse(parts[0], out var iterations)) return true;
            return iterations < DefaultIterations;
        }
    }
}
