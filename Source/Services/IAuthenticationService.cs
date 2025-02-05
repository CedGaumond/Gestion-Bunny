public interface IAuthenticationService
{
    Task<bool> LoginAsync(string email, string password);
}
