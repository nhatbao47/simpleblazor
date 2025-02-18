using SimpleBlazor.Services.ApiClient;

namespace SimpleBlazor.Services.GenericService;

public class AuthService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<AuthService> _logger;
    private readonly string _resource = "api/authentication";

    public AuthService(IApiClient apiClient, ILogger<AuthService> logger)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest model)
    {
        try
        {
            return await _apiClient.LoginAsync($"{_resource}/login", model);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while log in.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while log in.");
            throw;
        }
    }
}