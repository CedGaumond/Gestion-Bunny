using Gestion_Bunny.Services;
using Microsoft.EntityFrameworkCore;


public class AuthenticationService : IAuthenticationService
{
    private readonly EmployeeContext _context;
    private readonly AuthenticationState _authState;

    public AuthenticationService(EmployeeContext context, AuthenticationState authState)
    {
        _context = context;
        _authState = authState;
    }


    public async Task<bool> LoginAsync(string email, string password)
    {
        var employee = await _context.Employee
            .Include(e => e.EmployeeRole)
            .Where(e => e.Email == email)
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            return false;
        }

        bool isPasswordValid = password == employee.PasswordHash;

        if (isPasswordValid)
        {
            _authState.SetAuthenticated(employee);
        }

        return isPasswordValid;
    }


    public async Task LogoutAsync()
    {
        _authState.SetUnauthenticated();
        await Task.CompletedTask;
    }


    public string GenerateRandomPassword()
    {
        return "test";
    }

    public byte[] GenerateSaltedHash(byte[] passwordBytes, byte[] salt)
    {
        return null;
    }
}
