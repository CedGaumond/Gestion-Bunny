using System;
using System.Security.Cryptography;
using System.Text;

namespace Gestion_Bunny.Utils
{

    public static class CryptographyUtil
    {
        private const int SaltSize = 16; 
        private const int HashSize = 32;
        private const int Iterations = 100000;

        /// <summary>
        /// Génère un salt sécurisé (Base64).
        /// </summary>
        public static string CreateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Hache un mot de passe avec PBKDF2 et SHA256.
        /// </summary>
        public static string GenerateHash(string input, string saltBase64)
        {
            byte[] salt = Convert.FromBase64String(saltBase64);

            using (var pbkdf2 = new Rfc2898DeriveBytes(input, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Vérifie si un mot de passe correspond à son hash.
        /// </summary>
        public static bool AreEqual(string plainTextInput, string hashedInput, string saltBase64)
        {
            string newHashedPassword = GenerateHash(plainTextInput, saltBase64);
            return newHashedPassword.Equals(hashedInput);
        }
    }

}
