using Gestion_Bunny.Modeles;
using Gestion_Bunny.Utils;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Bunny.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<User> GetUsers()
    {
        return _context.Users.Include(s => s.UserRole).Include(e => e.Employee).ToList();
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId);
    }

    public Employee GetEmployeeById(int employeeId)
    {
        return _context.Employees.Find(employeeId);
    }

    public void ResetPassword(User user)
    {
        var tempPassword = PasswordGenerator.GenerateTemporaryPassword();
        var salt = CryptographyUtil.CreateSalt();
        user.PasswordHash = CryptographyUtil.GenerateHash(tempPassword, salt);
        user.PasswordSalt = salt;
        user.TempPassword = true;

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();

        Task.Run(async () =>
        {
            await EmailUtil.SendEmailAsync(
                user.Email,
                "Bienvenue chez Bunny & co - Réinitialisation du mot de passe",
                $"Bonjour {user.FirstName},\n\nVotre mot de passe temporaire est : {tempPassword}\n\nMerci de le changer dès votre première connexion."
            );
        }); 
    }

    public void UpdatePassword(User user, string inputPassword)
    {
        var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);

        if (existingUser != null)
        {
            _context.Entry(existingUser).State = EntityState.Detached;
        }

        var salt = CryptographyUtil.CreateSalt();
        user.PasswordHash = CryptographyUtil.GenerateHash(inputPassword, salt);
        user.PasswordSalt = salt;
        user.TempPassword = false; 

        _context.Entry(existingUser).State = EntityState.Modified; 

        _context.SaveChanges();
    }

    public void AddUser(User user)
    {
        var tempPassword = PasswordGenerator.GenerateTemporaryPassword();
        var salt = CryptographyUtil.CreateSalt();
        user.PasswordHash = CryptographyUtil.GenerateHash(tempPassword, salt);
        user.PasswordSalt = salt;
        user.TempPassword = true;

        _context.Users.Add(user);
        _context.SaveChanges();

        Task.Run(async () =>
        {
            await EmailUtil.SendEmailAsync(
                user.Email,
                "Bienvenue chez Bunny & co - Votre mot de passe temporaire",
                $"Bonjour {user.FirstName},\n\nVotre mot de passe temporaire est : {tempPassword}\n\nMerci de le changer dès votre première connexion."
            );
        });
    }

    public void UpdateUser(User user)
    {
        var userTemp = GetUserById(user.Id);

        if (userTemp != null && userTemp.IsDeleted != user.IsDeleted) 
        {
            user.DeletedDate = user.IsDeleted ? DateTime.UtcNow : (DateTime?)null;
        }

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        if(user.IsDeleted == false)
        {
            user.IsDeleted = true;
            user.DeletedDate = DateTime.UtcNow;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public UserRole? GetUserRoleByName(string name)
    {
        return _context.UserRoles.FirstOrDefault(ur => ur.RoleName == name);
    }

    public UserRole? GetUserRoleById(int id)
    {
        return _context.UserRoles.FirstOrDefault(ur => ur.Id == id);
    }

    public List<UserRole> GetUserRoles()
    {
        return _context.UserRoles.ToList();
    }

    public bool IsEmailExists(string email)
    {
        return _context.Users
            .Any(i => i.Email.ToLower() == email.ToLower());
    }

}
