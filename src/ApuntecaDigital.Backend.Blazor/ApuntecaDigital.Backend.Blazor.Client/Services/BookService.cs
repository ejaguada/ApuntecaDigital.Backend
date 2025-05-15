using System.Net.Http.Json;
using ApuntecaDigital.Backend.Blazor.Client.Models;
using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Classes;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;

namespace ApuntecaDigital.Backend.Blazor.Client.Services;

public class BookService
{
  private readonly HttpClient _httpClient;

  public BookService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<List<Book>> GetBooksAsync(string? title = null)
  {
    try
    {
      // Direct call to the Web API project
      string url = "/books";
      if (!string.IsNullOrWhiteSpace(title))
      {
        url = $"/Books/title/{Uri.EscapeDataString(title)}";
      }

      var response = await _httpClient.GetFromJsonAsync<BookListResponse>(url);
      return response?.Books?.Select(b => new Book { Id = b.Id, Title = b.Title, Author = b.Author, Isbn = b.Isbn, Subject = b.Subject }).ToList() ?? new List<Book>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching books: {ex.Message}");
      return new List<Book>();
    }
  }

  public async Task<Book?> GetBookByIdAsync(int id)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<Book>($"/books/{id}");
      return response == null ? null : new Book { Id = response.Id, Title = response.Title, Author = response.Author, Isbn = response.Isbn, Subject = response.Subject };
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching book by id {id}: {ex.Message}");
      return null;
    }
  }

  public async Task<bool> CreateBookAsync(CreateBook bookObj)
  {
    try
    {
      var response = await _httpClient.PostAsJsonAsync("/books", bookObj);
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error creating book: {ex.Message}");
      return false;
    }
  }

  public async Task<bool> UpdateBookAsync(UpdateBook bookObj)
  {
    try
    {
      var response = await _httpClient.PutAsJsonAsync($"/books/{bookObj.Id}", bookObj);
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error updating book: {ex.Message}");
      return false;
    }
  }
  public async Task<bool> DeleteBookAsync(int id)
  {
    try
    {
      var response = await _httpClient.DeleteAsync($"/books/{id}");
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error deleting book: {ex.Message}");
      return false;
    }
  }

  public async Task<List<Book>> GetBooksBySubjectIdAsync(int subjectId)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<BookListResponse>($"/books/subject/{subjectId}");
      return response?.Books?.Select(b => new Book { Id = b.Id, Title = b.Title, Author = b.Author, Isbn = b.Isbn }).ToList() ?? new List<Book>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching books by subject id {subjectId}: {ex.Message}");
      return new List<Book>();
    }
  }

  public async Task<List<Book>> GetBooksByNameAsync(string name)
  {
    try
    {
      var response = await _httpClient.GetFromJsonAsync<BookListResponse>($"/books/name/{Uri.EscapeDataString(name)}");
      return response?.Books?.Select(b => new Book { Id = b.Id, Title = b.Title, Author = b.Author, Isbn = b.Isbn }).ToList() ?? new List<Book>();
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine($"Error fetching books by name {name}: {ex.Message}");
      return new List<Book>();
    }
  }
}

