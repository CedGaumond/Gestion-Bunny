
using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services;
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task ResetPasswordAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<UserRole?> GetUserRoleByNameAsync(string name);
    Task<bool> IsEmailExists(string email);
    Task<List<UserRole>> GetUserRolesAsync();
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
}
