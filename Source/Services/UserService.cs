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

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Employees.FindAsync(employeeId);
    }

    public async Task ResetPasswordAsync(int userId)
    {
        /*TO DO*/
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        user.IsDeleted = true;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<UserRole?> GetUserRoleByNameAsync(string name)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(ur => ur.RoleName == name);
    }

    public async Task<List<UserRole>> GetUserRolesAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task<bool> IsEmailExists(string email)
    {
        return await _context.Users
            .AnyAsync(i => i.Email.ToLower() == email.ToLower());
    }

}
