using Gestion_Bunny.Modeles;
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
        return _context.Users.ToList();
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId);
    }

    public Employee GetEmployeeById(int employeeId)
    {
        return _context.Employees.Find(employeeId);
    }

    public void ResetPassword(int userId)
    {
        /*TO DO*/
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        var userTemp = GetUserById(user.Id);

        if (userTemp != null && userTemp.IsDeleted != user.IsDeleted) // Vérification si l'état de suppression a changé
        {
            user.DeletedDate = user.IsDeleted ? DateTime.UtcNow : (DateTime?)null; // Si supprimé, on définit la date, sinon on la supprime
        }

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        if(user.IsDeleted = false)
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
