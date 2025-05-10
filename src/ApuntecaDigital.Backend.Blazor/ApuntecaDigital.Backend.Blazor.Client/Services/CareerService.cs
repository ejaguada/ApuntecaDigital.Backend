﻿using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Models;
using ApuntecaDigital.Backend.UseCases.Careers;

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
                url = $"/Careers/name/{Uri.EscapeDataString(name)}";
            }

            var response = await _httpClient.GetFromJsonAsync<CareerListResponse>(url);
            return response?.Careers?.Select(c => new Career { Id = c.Id, Name = c.Name }).ToList() ?? new List<Career>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching careers: {ex.Message}");
            return new List<Career>();
        }
    }

    public async Task<Career?> GetCareerByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<CareerDTO>($"/careers/id/{id}");
            return response == null ? null : new Career { Id = response.Id, Name = response.Name };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error fetching career by id {id}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateCareerAsync(Career career)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/careers", new CareerDTO(0, career.Name));
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating career: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCareerAsync(Career career)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/careers/{career.Id}", new CareerDTO(career.Id, career.Name));
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error updating career: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCareerAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/careers/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error deleting career with id {id}: {ex.Message}");
            return false;
        }
    }
}
