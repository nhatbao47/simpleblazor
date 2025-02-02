using SimpleBlazor.Services.ApiClient;

namespace SimpleBlazor.Services.GenericService;

public class UserService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<UserService> _logger;
    private readonly string _resource = "api/users";

    public UserService(IApiClient apiClient, ILogger<UserService> logger)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    public async Task<List<UserModel>> GetUsersAsync()
    {
        try
        {
            var users = await _apiClient.GetAllAsync<UserModel>(_resource);
            return users ?? [];
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching users.");
            return [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching users.");
            return [];
        }
    }

    public async Task<bool> UpdateUserAsync(UserModel updatedUser)
    {
        try
        {
            return await _apiClient.UpdateAsync($"{_resource}/{updatedUser.Id}", updatedUser);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while updating the user.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while updating the user.");
            throw;
        }
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        try
        {
            return await _apiClient.DeleteAsync(_resource, id);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting user with ID {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while deleting user with ID {id}.");
            throw;
        }
    }
}