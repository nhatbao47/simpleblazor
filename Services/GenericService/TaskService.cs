using SimpleBlazor.Services.ApiClient;

namespace SimpleBlazor.Services.GenericService;

public class TaskService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<TaskService> _logger;
    private const string Resource = "api/tasks";

    public TaskService(IApiClient apiClient, ILogger<TaskService> logger)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<List<SimpleTask>> GetTasksAsync()
    {
        try
        {
            var tasks = await _apiClient.GetAllAsync<SimpleTask>(Resource);
            return tasks ?? [];
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching products.");
            return [];
        }
        catch ( Exception ex )
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching products.");
            return  [];
        }
    }

    public async Task<SimpleTask> GetTaskByIdAsync(int id)
    {
        try
        {
            var task = await _apiClient.GetByIdAsync<SimpleTask>(Resource, id);
            return task;
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching task with ID {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while fetching task with ID {id}.");
            throw;
        }
    }

    public async Task<SimpleTask> CreateTaskAsync(SimpleTask newTask)
    {
        try
        {
            var createdTask = await _apiClient.InsertAsync(Resource, newTask);
            return createdTask;
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new task.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while creating a new task.");
            throw;
        }
    }

    public async Task<SimpleTask> UpdateTaskAsync(SimpleTask updatedTask)
    {
        try
        {
            var task = await _apiClient.UpdateAsync(Resource, updatedTask);
            return task;
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while updating the task.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while updating the task.");
            throw;
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        try
        {
            return await _apiClient.DeleteAsync(Resource, id);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting task with ID {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while deleting task with ID {id}.");
            throw;
        }
    }
}