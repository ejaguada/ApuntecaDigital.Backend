namespace ApuntecaDigital.Backend.Blazor.Client.Models;

public class UpdateBook
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public string Isbn { get; set; } = string.Empty;
  public int SubjectId { get; set; }
}
