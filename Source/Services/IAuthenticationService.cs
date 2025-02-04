public interface IAuthenticationService
{
    Task<bool> LoginAsync(string email, string password);

    byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);

    string generateRandomPassword();
}
