using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Models;

namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public class CareerService
{
    private readonly HttpClient _httpClient;

    public CareerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Career>> GetCareersAsync(string? name = null)
    {
        try
        {
            // Direct call to the Web API project
            string url = "/careers";
            if (!string.IsNullOrWhiteSpace(name))
            {
                url += $"?name={Uri.EscapeDataString(name)}";
            }

            var response = await _httpClient.GetFromJsonAsync<CareerListResponse>(url);
            return response?.Careers ?? new List<Career>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching careers: {ex.Message}");
            return new List<Career>();
        }
    }
}
