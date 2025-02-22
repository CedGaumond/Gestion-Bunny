using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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

        // TODO: password hasing fr
        bool isPasswordValid = password == user.PasswordHash;

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

    public string GenerateRandomPassword()
    {

        Random rand = new Random();

        int stringlen = rand.Next(6, 8);
        int randValue;
        string str = "";
        char letter;
        for (int i = 0; i < stringlen; i++)
        {

            randValue = rand.Next(0, 26);

            letter = Convert.ToChar(randValue + 65);

            str = str + letter;
        }

        return str;

    }

    public byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
    {
        using var algorithm = SHA256.Create();

        byte[] plainTextWithSaltBytes =
            new byte[plainText.Length + salt.Length];

        for (int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText[i];
        }
        for (int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

}
