using Gestion_Bunny.Modeles;
namespace Gestion_Bunny.Services;

public class AuthenticationState
{
    public bool IsAuthenticated { get; private set; }
    public Employee? CurrentUser { get; private set; }

    public event Action OnAuthenticationStateChanged;

    public void SetAuthenticated(Employee user)
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
}