using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Models;
using ApuntecaDigital.Backend.UseCases.Subjects;
namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public class SubjectService
{
  private readonly HttpClient _httpClient;

  public SubjectService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<List<Subject>> GetSubjectsAsync(string? name = null)
  {
    try
    {
      // Direct call to the Web API project
      string url = "/subjects";
      if (!string.IsNullOrWhiteSpace(name))
      {
        url = $"/subjects/name/{Uri.EscapeDataString(name)}";
      }

      var response = await _httpClient.GetFromJsonAsync<SubjectListResponse>(url);
      return response?.Subjects?.Select(s => new Subject { Id = s.Id, Name = s.Name, Class = new Class { Id = s.Class.Id, Name = s.Class.Name } }).ToList() ?? new List<Subject>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching subjects: {ex.Message}");
      return new List<Subject>();
    }
  }

  public async Task<Subject?> GetSubjectByIdAsync(int id)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<SubjectDTO>($"/subjects/id/{id}");
      return response == null ? null : new Subject { Id = response.Id, Name = response.Name, Class = new Class { Id = response.ClassId, Name = response.Name } };
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching subject by id {id}: {ex.Message}");
      return null;
    }
  }

  public async Task<bool> CreateSubjectAsync(Subject subject)
  {
    try
    {
      var response = await _httpClient.PostAsJsonAsync("/subjects", new UpdateSubjectDTO(0, subject.Name, subject.Class.Id));
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error creating subject: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> UpdateSubjectAsync(Subject subject)
  {
    try
    {
      var response = await _httpClient.PutAsJsonAsync($"/subjects/{subject.Id}", new UpdateSubjectDTO(subject.Id, subject.Name, subject.Class.Id));
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error updating subject: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> DeleteSubjectAsync(int id)
  {
    try
    {
      var response = await _httpClient.DeleteAsync($"/subjects/{id}");
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error deleting subject: {ex.Message}");
      return false;
    }
  }

  public async Task<List<Subject>> GetSubjectsByCareerIdAsync(int careerId)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<SubjectListResponse>($"/subjects/career/{careerId}");
      return response?.Subjects?.Select(s => new Subject { Id = s.Id, Name = s.Name, Class = new Class { Id = s.Class.Id, Name = s.Class.Name } }).ToList() ?? new List<Subject>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching subjects by career id {careerId}: {ex.Message}");
      return new List<Subject>();
    }
  }

  public async Task<List<Subject>> GetSubjectsByNameAsync(string name)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<SubjectListResponse>($"/subjects/name/{Uri.EscapeDataString(name)}");
      return response?.Subjects?.Select(s => new Subject { Id = s.Id, Name = s.Name, Class = new Class { Id = s.Class.Id, Name = s.Class.Name } }).ToList() ?? new List<Subject>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching subjects by name {name}: {ex.Message}");
      return new List<Subject>();
    }
  }

  public async Task<List<Subject>> GetSubjectsByClassIdAsync(int classId)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<SubjectListResponse>($"/subjects/filter-by/class/id/{Uri.EscapeDataString(classId.ToString())}");
      return response?.Subjects?.Select(s => new Subject { Id = s.Id, Name = s.Name, Class = new Class { Id = s.Class.Id, Name = s.Class.Name } }).ToList() ?? new List<Subject>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching subjects by class id {classId}: {ex.Message}");
      return new List<Subject>();
    }
  }
}

