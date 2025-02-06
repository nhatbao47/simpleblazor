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
}