using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


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


    public string generateRandomPassword(){
            
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
    HashAlgorithm algorithm = new SHA256Managed();

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
