
using Gestion_Bunny.Modeles;

namespace Gestion_Bunny.Services;
public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int userId);
    Task ResetPasswordAsync(int userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
}
