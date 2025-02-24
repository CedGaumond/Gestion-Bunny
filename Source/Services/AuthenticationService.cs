using Gestion_Bunny.Utils;

namespace Gestion_Bunny.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ApplicationDbContext _context;
    private readonly AuthenticationState _authState;

    public AuthenticationService(ApplicationDbContext context, AuthenticationState authState)
    {
        _context = context;
        _authState = authState;
    }

    public bool Login(string email, string password)
    {
        var user =  _context.Users.Where(e => e.Email == email).FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        bool isPasswordValid = CryptographyUtil.AreEqual(password, user.PasswordHash, user.PasswordSalt);

        if (isPasswordValid)
        {
            _authState.SetAuthenticated(user);
        }

        return isPasswordValid;
    }


    public void Logout()
    {
        _authState.SetUnauthenticated();
    }
}
