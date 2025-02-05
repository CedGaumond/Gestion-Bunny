public interface IAuthenticationService
{
    Task<bool> LoginAsync(string email, string password);
    Task LogoutAsync();
    string GenerateRandomPassword();
    byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);
}
