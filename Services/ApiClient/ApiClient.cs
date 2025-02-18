namespace SimpleBlazor.Services.ApiClient;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;

public class ApiClient: IApiClient 
{
    private readonly HttpClient _httpClient;
    private readonly StateContainer _stateContainer;
    
    public ApiClient(HttpClient httpClient, StateContainer stateContainer)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _stateContainer = stateContainer ?? throw new ArgumentNullException(nameof(stateContainer));
    }

    public async Task<List<T>> GetAllAsync<T>(string url)
    {
        AddAuthorizationHeader();

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<T>>() ?? [];
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Network error: {ex.Message}");
            throw new ApplicationException("There was an issue connecting to the API.", ex);
        }
        catch (JsonException ex)
        {
            Console.Error.WriteLine($"JSON error: {ex.Message}");
            throw new ApplicationException("There was an issue processing the data.", ex);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred while fetching data.", ex);
        }
    }

    public async Task<T> GetByIdAsync<T>(string url, int id)
    {
        AddAuthorizationHeader();

        try {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new ApplicationException("The response content was empty.");
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Network error: {ex.Message}");
            throw new ApplicationException("Failed to connect to the API.", ex);
        }
        catch(JsonException ex)
        {
            Console.Error.WriteLine($"JSON deserialization error: {ex.Message}");
            throw new ApplicationException("Failed to deserialize the response", ex);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            throw new ApplicationException("An unexpected error occurred while fetching data.", ex);
        }
    }

    public async Task<T> InsertAsync<T>(string url, T model)
    {
        AddAuthorizationHeader();

        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>()
                ?? throw new ApplicationException("The response content was empty.");
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in InsertAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> UpdateAsync<T>(string url, T model)
    {
        AddAuthorizationHeader();

        try
        {
            var response = await _httpClient.PutAsJsonAsync(url, model);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in UpdateAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string url, int id)
    {
        AddAuthorizationHeader();

        try
        {
            var response = await _httpClient.DeleteAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in DeleteAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<LoginResponse> LoginAsync(string url, LoginRequest model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(url, model);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResponse>()
                ?? throw new ApplicationException("The response content was empty.");
        }
        catch(Exception ex)
        {
            Console.Error.WriteLine($"Error in LoginAsync: {ex.Message}");
            throw;
        }
    }

    private void AddAuthorizationHeader()
    {
        if (!string.IsNullOrWhiteSpace(_stateContainer.AuthToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _stateContainer.AuthToken);
        }
    }
}