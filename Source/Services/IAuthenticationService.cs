public interface IAuthenticationService
{
    bool Login(string email, string password);
    void Logout();
}
