using Gestion_Bunny.Modeles;
namespace Gestion_Bunny.Services;

public class AuthenticationState
{
    public bool IsAuthenticated { get; private set; }
    public User? CurrentUser { get; private set; }

    public event Action OnAuthenticationStateChanged = delegate { };

    private readonly IUserService _userService;
    public AuthenticationState(IUserService userService)
    {
        _userService = userService;
    }

    public void SetAuthenticated(User user)
    {
        IsAuthenticated = true;
        CurrentUser = user;
        NotifyStateChanged();
    }

    public void SetUnauthenticated()
    {
        IsAuthenticated = false;
        CurrentUser = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnAuthenticationStateChanged?.Invoke();

    public UserRole GetCurrentUserRole()
    {
        if (CurrentUser != null)
        {
            if(CurrentUser.UserRoleId != null)
            {
                var role = _userService.GetUserRoleById((int)CurrentUser.UserRoleId);
                if (role != null)
                    return role;
            }

        }
        return null;
    }
}