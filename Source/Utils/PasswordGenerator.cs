using System.Security.Cryptography;
using System.Text;

namespace Gestion_Bunny.Utils
{
    public class PasswordGenerator
    {
        private static readonly string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string Digits = "0123456789";
        private static readonly string SpecialChars = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        public static string GenerateTemporaryPassword(int length = 8)
        {
            if (length < 6) throw new ArgumentException("Le mot de passe doit avoir au moins 6 caractères.");

            string allChars = Uppercase + Lowercase + Digits + SpecialChars;
            StringBuilder password = new StringBuilder();
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            // Ajout de caractères pour garantir la diversité
            password.Append(GetRandomChar(Uppercase, rng));
            password.Append(GetRandomChar(Lowercase, rng));
            password.Append(GetRandomChar(Digits, rng));
            password.Append(GetRandomChar(SpecialChars, rng));

            // Remplissage du reste du mot de passe
            for (int i = 4; i < length; i++)
            {
                password.Append(GetRandomChar(allChars, rng));
            }

            // Mélange les caractères pour éviter un motif prévisible
            return new string(password.ToString().OrderBy(_ => GetRandomInt(rng)).ToArray());
        }

        private static char GetRandomChar(string source, RandomNumberGenerator rng)
        {
            byte[] buffer = new byte[1];
            rng.GetBytes(buffer);
            int index = buffer[0] % source.Length;
            return source[index];
        }

        private static int GetRandomInt(RandomNumberGenerator rng)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0) & int.MaxValue;
        }
    }
}
