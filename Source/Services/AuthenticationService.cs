using Gestion_Bunny.Modeles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class AuthenticationService : IAuthenticationService
{
    private readonly EmployeeContext _context;

    public AuthenticationService(EmployeeContext context)
    {
        _context = context;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        
        var employee = await _context.Employee
            .Where(e => e.Email == email)
            .FirstOrDefaultAsync();

        if (employee == null)
        {
            // User not found
            return false;
        }

        
        bool isPasswordValid = password == employee.PasswordHash;
        return isPasswordValid;
    }
}
