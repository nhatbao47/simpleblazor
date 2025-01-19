namespace SimpleBlazor.Services.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IApiClient
{
    Task<List<T>> GetAllAsync<T>(string url);
    Task<T> GetByIdAsync<T>(string url, int id);
    Task<T> InsertAsync<T>(string url, T model);
    Task<bool> DeleteAsync(string url, int id);
    Task<T> UpdateAsync<T>(string url, T model);
}