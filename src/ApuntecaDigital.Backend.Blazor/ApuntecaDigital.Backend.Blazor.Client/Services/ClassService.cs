using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Models;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public class ClassService
{
  private readonly HttpClient _httpClient;

  public ClassService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<List<Class>> GetClassesAsync(string? name = null)
  {
    try
    {
      // Direct call to the Web API project
      string url = "/classes";
      if (!string.IsNullOrWhiteSpace(name))
      {
        url = $"/Classes/name/{Uri.EscapeDataString(name)}";
      }

      var response = await _httpClient.GetFromJsonAsync<ClassListResponse>(url);
      return response?.Classes?.Select(c => new Class { Id = c.Id, Name = c.Name, Year = c.Year, CareerId = c.CareerId }).ToList() ?? new List<Class>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching classes: {ex.Message}");
      return new List<Class>();
    }
  }

  public async Task<Class?> GetClassByIdAsync(int id)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<ClassDTO>($"/classes/id/{id}");
      return response == null ? null : new Class { Id = response.Id, Name = response.Name, Year = response.Year, CareerId = response.CareerId };
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching class by id {id}: {ex.Message}");
      return null;
    }
  }

  public async Task<bool> CreateClassAsync(Class classObj)
  {
    await Task.Delay(1000);
    try
    {
      // var response = await _httpClient.PostAsJsonAsync("/classes", new ClassDTO(0, classObj.Name, classObj.Year, classObj.CareerId));
      // return response.IsSuccessStatusCode;
      return false;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error creating class: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> UpdateClassAsync(Class classObj)
  {
    await Task.Delay(1000);
    try
    {
      // var response = await _httpClient.PutAsJsonAsync($"/classes/{classObj.Id}", new ClassDTO(classObj.Id, classObj.Name, classObj.Year, classObj.CareerId));
      // return response.IsSuccessStatusCode;
      //return false;
      return false;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error updating class: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> DeleteClassAsync(int id)
  {
    try
    {
      var response = await _httpClient.DeleteAsync($"/classes/{id}");
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error deleting class: {ex.Message}");
      return false;
    }
  }

  public async Task<List<Class>> GetClassesByCareerIdAsync(int careerId)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<ClassListResponse>($"/classes/career/{careerId}");
      return response?.Classes?.Select(c => new Class { Id = c.Id, Name = c.Name, Year = c.Year, CareerId = c.CareerId }).ToList() ?? new List<Class>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching classes by career id {careerId}: {ex.Message}");
      return new List<Class>();
    }
  }

  public async Task<List<Class>> GetClassesByNameAsync(string name)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<ClassListResponse>($"/classes/name/{Uri.EscapeDataString(name)}");
      return response?.Classes?.Select(c => new Class { Id = c.Id, Name = c.Name, Year = c.Year, CareerId = c.CareerId }).ToList() ?? new List<Class>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching classes by name {name}: {ex.Message}");
      return new List<Class>();
    }
  }
}

