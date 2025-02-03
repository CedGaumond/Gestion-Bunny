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

            return false;
        }

        //TODO : faire l,algo pour hasher les mots de passe 
        bool isPasswordValid = password == employee.PasswordHash;
        return isPasswordValid;
    }
}
