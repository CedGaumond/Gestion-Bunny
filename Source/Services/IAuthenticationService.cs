public interface IAuthenticationService
{
    bool Login(string email, string password);
    void Logout();
    string GenerateRandomPassword();
    byte[] GenerateSaltedHash(byte[] plainText, byte[] salt);
}
