
using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services;
public interface IUserService
{
    List<User> GetUsers();
    User GetUserById(int userId);
    void ResetPassword(int userId);
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
    UserRole? GetUserRoleByName(string name);
    bool IsEmailExists(string email);
    List<UserRole> GetUserRoles();
    Employee GetEmployeeById(int employeeId);
}
