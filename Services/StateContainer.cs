using System.Runtime.CompilerServices;

public class StateContainer
{
    private string? authUser;

    public string AuthUser
    {
        get => authUser ?? string.Empty;
        set
        {
            authUser = value;
            NotifyStateChanged();
        }
    }

    public string? AuthToken { get; set; }

    public event Action? OnChange;

    public bool IsAuthenticated => !string.IsNullOrEmpty(authUser);

    public void Clear()
    {
        authUser = null;
        AuthToken = null;
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}