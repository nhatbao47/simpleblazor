using SimpleBlazor.Services.ApiClient;

namespace SimpleBlazor.Services.GenericService;

public class ScheduleService
{
    private readonly IApiClient _apiClient;
    private readonly ILogger<ScheduleService> _logger;
    private readonly string _resource = "api/schedules";

    public ScheduleService(IApiClient apiClient, ILogger<ScheduleService> logger)
    {
        _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    public async Task<List<ScheduleModel>> GetSchedulesAsync()
    {
        try
        {
            var schedules = await _apiClient.GetAllAsync<ScheduleModel>(_resource);
            return schedules ?? [];
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while fetching schedules.");
            return [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while fetching schedules.");
            return [];
        }
    }

    public async Task<ScheduleModel> GetScheduleByIdAsync(int id)
    {
        try
        {
            var schedule = await _apiClient.GetByIdAsync<ScheduleModel>(_resource, id);
            return schedule;
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching schedule with ID {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while fetching schedule with ID {id}.");
            throw;
        }
    }

    public async Task<ScheduleModel> CreateScheduleAsync(ScheduleModel newSchedule)
    {
        try
        {
            return await _apiClient.InsertAsync(_resource, newSchedule);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new schedule.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while creating a new schedule.");
            throw;
        }
    }

    public async Task<bool> UpdateScheduleAsync(ScheduleModel updatedSchedule)
    {
        try
        {
            return await _apiClient.UpdateAsync($"{_resource}/{updatedSchedule.Id}", updatedSchedule);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, "Error occurred while updating the schedule.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while updating the schedule.");
            throw;
        }
    }

    public async Task<bool> DeleteScheduleAsync(int id)
    {
        try
        {
            return await _apiClient.DeleteAsync(_resource, id);
        }
        catch (ApplicationException ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting schedule with ID {id}.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while schedule task with ID {id}.");
            throw;
        }
    }
}